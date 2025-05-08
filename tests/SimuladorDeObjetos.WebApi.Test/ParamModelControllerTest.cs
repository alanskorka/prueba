using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;
namespace SimuladorDeObjetos.WepApi.Test;

[TestClass]
public class ParamModelControllerTest
{
    private Mock<IParamModelService>? _mockService;
    private ParamModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IParamModelService>();
        _controller = new ParamModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllParams()
    {
        var expected = new List<ParamModel>
        {
            new ParamModel { Name = "param1", Type = "string" },
            new ParamModel { Name = "param2", Type = "int" }
        };
        _mockService.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<ParamModel>;
        Assert.IsNotNull(returned);
        Assert.AreEqual(2, returned.Count());
    }

    [TestMethod]
    public void Add_ShouldCallAddMethod()
    {
        var model = new ParamModel { Name = "param", Type = "bool" };

        var result = _controller.Add(model);

        _mockService.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldCallUpdateMethod()
    {
        var model = new ParamModel { Name = "param", Type = "bool" };

        var result = _controller.Update(model);

        _mockService.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldCallDeleteMethod()
    {
        var model = new ParamModel { Name = "param", Type = "bool" };

        var result = _controller.Delete(model);

        _mockService.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void GetAll_ShouldReturnProblem_WhenServiceThrows()
    {
        _mockService!.Setup(s => s.GetAll()).Throws(new Exception("fail"));

        var result = _controller!.GetAll();

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Add_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new ParamModel();

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Add_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new ParamModel { Name = "param", Type = "int" };
        _mockService!.Setup(s => s.Add(model)).Throws(new Exception("fail"));

        var result = _controller!.Add(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Type", "Required");
        var model = new ParamModel();

        var result = _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new ParamModel { Name = "param", Type = "int" };
        _mockService!.Setup(s => s.Update(model)).Throws(new Exception("fail"));

        var result = _controller!.Update(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new ParamModel { Name = "param", Type = "bool" };
        _mockService!.Setup(s => s.Delete(model)).Throws(new Exception("fail"));

        var result = _controller!.Delete(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
