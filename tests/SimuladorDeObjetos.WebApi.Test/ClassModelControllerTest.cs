using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Services.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class ClassModelControllerTest
{
    private Mock<IClassModelService>? _mockService;
    private ClassModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IClassModelService>();
        _controller = new ClassModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllClasses()
    {
        var expected = new List<ClassModel>
        {
            new ClassModel { Name = "Class1" },
            new ClassModel { Name = "Class2" }
        };

        _mockService!.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller!.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<ClassModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var model = new ClassModel { Name = "NewClass" };

        var result = _controller!.Add(model);

        _mockService!.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var model = new ClassModel { Name = "UpdateClass" };

        var result = _controller!.Update(model);

        _mockService!.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var model = new ClassModel { Name = "ToDelete" };

        var result = _controller!.Delete(model);

        _mockService!.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Add_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new ClassModel();

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new ClassModel();

        var result = _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void GetAll_ShouldReturnProblem_WhenServiceThrowsException()
    {
        _mockService!.Setup(s => s.GetAll()).Throws(new Exception("fail"));

        var result = _controller!.GetAll();

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Add_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new ClassModel { Name = "ErrorClass" };
        _mockService!.Setup(s => s.Add(model)).Throws(new Exception("fail"));

        var result = _controller!.Add(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new ClassModel { Name = "ErrorClass" };
        _mockService!.Setup(s => s.Update(model)).Throws(new Exception("fail"));

        var result = _controller!.Update(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new ClassModel { Name = "ErrorClass" };
        _mockService!.Setup(s => s.Delete(model)).Throws(new Exception("fail"));

        var result = _controller!.Delete(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
