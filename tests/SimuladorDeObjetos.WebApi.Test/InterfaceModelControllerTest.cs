using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class InterfaceModelControllerTest
{
    private Mock<IInterfaceModelService>? _mockService;
    private InterfaceModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IInterfaceModelService>();
        _controller = new InterfaceModelController(_mockService.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllInterfaces()
    {
        var expected = new List<InterfaceModel>
        {
            new InterfaceModel { Name = "I1" },
            new InterfaceModel { Name = "I2" }
        };

        _mockService!.Setup(s => s.GetAll()).ReturnsAsync(expected);

        var result = await _controller!.GetAll();
        var okResult = result.Result as OkObjectResult;

        Assert.IsNotNull(okResult);
        var returned = okResult.Value as IEnumerable<InterfaceModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public async Task Add_ShouldReturnNoContent()
    {
        var model = new InterfaceModel { Name = "NewInterface" };

        var result = await _controller!.Add(model);

        _mockService!.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task Update_ShouldReturnNoContent()
    {
        var model = new InterfaceModel { Name = "UpdatedInterface" };

        var result = await _controller!.Update(model);

        _mockService!.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task Delete_ShouldReturnNoContent()
    {
        var model = new InterfaceModel { Name = "ToDelete" };

        var result = await _controller!.Delete(model);

        _mockService!.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task Add_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new InterfaceModel();

        var result = await _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new InterfaceModel();

        var result = await _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnProblem_WhenServiceThrowsException()
    {
        _mockService!.Setup(s => s.GetAll()).ThrowsAsync(new Exception("fail"));

        var result = await _controller!.GetAll();

        Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result.Result).StatusCode);
    }

    [TestMethod]
    public async Task Add_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new InterfaceModel { Name = "ErrorInterface" };
        _mockService!.Setup(s => s.Add(model)).ThrowsAsync(new Exception("fail"));

        var result = await _controller!.Add(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public async Task Update_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new InterfaceModel { Name = "ErrorInterface" };
        _mockService!.Setup(s => s.Update(model)).ThrowsAsync(new Exception("fail"));

        var result = await _controller!.Update(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public async Task Delete_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new InterfaceModel { Name = "ErrorInterface" };
        _mockService!.Setup(s => s.Delete(model)).ThrowsAsync(new Exception("fail"));

        var result = await _controller!.Delete(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
