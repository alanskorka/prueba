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

        _mockService.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<MethodCallModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Create_ShouldReturnOk()
    {
        var methodCall = new MethodCallModel { MethodName = "Call3", ReferenceType = ReferenceTypeInvocation.Parameter };

        var result = _controller.Create(methodCall);

        _mockService.Verify(s => s.Create(methodCall), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var methodCall = new MethodCallModel { MethodName = "Call4", ReferenceType = ReferenceTypeInvocation.Base };

        var result = _controller.Update(methodCall);

        _mockService.Verify(s => s.Update(methodCall), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var methodCall = new MethodCallModel { MethodName = "Call4", ReferenceType = ReferenceTypeInvocation.This };

        var result = _controller.Delete(methodCall);

        _mockService.Verify(s => s.Delete(methodCall), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}
