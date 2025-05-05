using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.test;

[TestClass]
public class MethodModelControllerTest
{
    private Mock<IMethodModelService>? _mockService;
    private MethodModelController? _controller;

    [TestInitialize]
    public void Init()
    {
        _mockService = new Mock<IMethodModelService>();
        _controller = new MethodModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllMethods()
    {
        var expected = new List<MethodModel>
        {
            new MethodModel { Name = "Method1" },
            new MethodModel { Name = "Method2" }
        };

        _mockService!.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller!.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<MethodModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var method = new MethodModel { Name = "NewMethod" };

        var result = _controller!.Add(method);

        _mockService!.Verify(s => s.Add(method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var method = new MethodModel { Name = "UpdatedMethod" };

        var result = _controller!.Update(method);

        _mockService!.Verify(s => s.Update(method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var method = new MethodModel { Name = "ToDelete" };

        var result = _controller!.Delete(method);

        _mockService!.Verify(s => s.Delete(method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void AddToClass_ShouldCallServiceWithCorrectParameters_andReturnOk()
    {
        var classId = Guid.NewGuid();
        var method = new MethodModel { Id = Guid.NewGuid(), Name = "NewInClass" };
        var result = _controller!.AddToClass(classId, method);
        _mockService!.Verify(s => s.AddMethodToClass(classId, method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}
