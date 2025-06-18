using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WepApi.Test;

[TestClass]
public class MethodCallModelControllerTest
{
    private Mock<IMethodCallModelService>? _mockService;
    private MethodCallModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IMethodCallModelService>();
        _controller = new MethodCallModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllMethodCalls()
    {
        var expected = new List<MethodCallModel>
        {
            new MethodCallModel { MethodName = "Call1", ReferenceType = ReferenceTypeInvocation.This },
            new MethodCallModel { MethodName = "Call2", ReferenceType = ReferenceTypeInvocation.Attribute }
        };

        _mockService!.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller!.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<MethodCallModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void GetById_ShouldReturnMethodCall_WhenExists()
    {
        var expected = new MethodCallModel { Id = Guid.NewGuid(), MethodName = "Call", ReferenceType = ReferenceTypeInvocation.This };
        _mockService!.Setup(s => s.GetById(expected.Id)).Returns(expected);

        var result = _controller!.GetById(expected.Id) as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as MethodCallModel;
        Assert.IsNotNull(returned);
        Assert.AreEqual(expected.Id, returned.Id);
    }

    [TestMethod]
    public void GetById_ShouldReturnNotFound_WhenNotExists()
    {
        var id = Guid.NewGuid();
        _mockService!.Setup(s => s.GetById(id)).Returns((MethodCallModel?)null);

        var result = _controller!.GetById(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public void Create_ShouldReturnOk()
    {
        var methodCall = new MethodCallModel { MethodName = "Call3", ReferenceType = ReferenceTypeInvocation.Parameter };

        var result = _controller!.Create(methodCall);

        _mockService!.Verify(s => s.Create(methodCall), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var id = Guid.NewGuid();
        var methodCall = new MethodCallModel { Id = id, MethodName = "Call4", ReferenceType = ReferenceTypeInvocation.Base };

        var result = _controller!.Update(id, methodCall);

        _mockService!.Verify(s => s.Update(methodCall), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var id = Guid.NewGuid();
        var methodCall = new MethodCallModel { Id = id, MethodName = "Call4", ReferenceType = ReferenceTypeInvocation.This };
        _mockService!.Setup(s => s.GetById(id)).Returns(methodCall);

        var result = _controller!.Delete(id);

        _mockService!.Verify(s => s.GetById(id), Times.Once);
        _mockService!.Verify(s => s.Delete(methodCall), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnNotFound_WhenNotExists()
    {
        var id = Guid.NewGuid();
        _mockService!.Setup(s => s.GetById(id)).Returns((MethodCallModel?)null);

        var result = _controller!.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public void Create_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("MethodName", "Required");
        var model = new MethodCallModel();

        var result = _controller.Create(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("MethodName", "Required");
        var model = new MethodCallModel();

        var result = _controller.Update(Guid.NewGuid(), model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
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
    public void Create_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new MethodCallModel { MethodName = "Throw", ReferenceType = ReferenceTypeInvocation.LocalVar };
        _mockService!.Setup(s => s.Create(model)).Throws(new Exception("fail"));

        var result = _controller!.Create(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrows()
    {
        var id = Guid.NewGuid();
        var model = new MethodCallModel { Id = id, MethodName = "Throw", ReferenceType = ReferenceTypeInvocation.Base };
        _mockService!.Setup(s => s.Update(model)).Throws(new Exception("fail"));

        var result = _controller!.Update(id, model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrows()
    {
        var id = Guid.NewGuid();
        var model = new MethodCallModel { Id = id, MethodName = "Throw", ReferenceType = ReferenceTypeInvocation.Base };
        _mockService!.Setup(s => s.GetById(id)).Returns(model);
        _mockService!.Setup(s => s.Delete(model)).Throws(new Exception("fail"));

        var result = _controller!.Delete(id);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
