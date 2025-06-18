using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class InterfaceMethodModelControllerTest
{
    private Mock<IInterfaceMethodModelService> _mockService = null!;
    private InterfaceMethodModelController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IInterfaceMethodModelService>();
        _controller = new InterfaceMethodModelController(_mockService.Object);
    }

    [TestMethod]
    public async Task Add_ShouldCallServiceAndReturnOk()
    {
        var model = new InterfaceMethodModel
        {
            Name = "TestMethod",
            ReturnType = "void"
        };

        var result = await _controller.Add(model);

        _mockService.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllInterfaceMethods()
    {
        var expected = new List<InterfaceMethodModel>
        {
            new InterfaceMethodModel
            {
                Name = "Method1",
                ReturnType = "void"
            },
            new InterfaceMethodModel
            {
                Name = "Method2",
                ReturnType = "string"
            }
        };

        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(expected);

        var result = await _controller.GetAll();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var returnedMethods = okResult.Value as IEnumerable<InterfaceMethodModel>;
        Assert.IsNotNull(returnedMethods);
        Assert.AreEqual(2, returnedMethods.Count());

        var methodsList = returnedMethods.ToList();
        Assert.AreEqual("Method1", methodsList[0].Name);
        Assert.AreEqual("Method2", methodsList[1].Name);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnInterfaceMethod_WhenExists()
    {
        var expected = new InterfaceMethodModel
        {
            Id = 1,
            Name = "TestMethod",
            ReturnType = "string"
        };

        _mockService.Setup(s => s.GetById(expected.Id))
            .ReturnsAsync(expected);

        var result = await _controller.GetById(expected.Id);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var returnedMethod = okResult.Value as InterfaceMethodModel;
        Assert.IsNotNull(returnedMethod);
        Assert.AreEqual(expected.Id, returnedMethod.Id);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnNotFound_WhenNotExists()
    {
        var id = 1;
        _mockService.Setup(s => s.GetById(id))
            .ReturnsAsync((InterfaceMethodModel?)null);

        var result = await _controller.GetById(id);

        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Update_ShouldCallServiceAndReturnOk()
    {
        var id = 1;
        var model = new InterfaceMethodModel
        {
            Id = id,
            Name = "UpdatedMethod",
            ReturnType = "string"
        };

        var result = await _controller.Update(id, model);

        _mockService.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task Delete_ShouldCallServiceAndReturnOk()
    {
        var id = 1;

        var result = await _controller.Delete(id);

        _mockService.Verify(s => s.Delete(id), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}