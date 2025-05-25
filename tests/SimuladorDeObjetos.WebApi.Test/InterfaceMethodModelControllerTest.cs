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
    public async Task Add_ShouldCallServiceAndReturnNoContent()
    {
        var model = new InterfaceMethodModel
        {
            Name = "TestMethod",
            ReturnType = "void"
        };

        var result = await _controller.Add(model);

        _mockService.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
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
    public async Task Update_ShouldCallServiceAndReturnNoContent()
    {
        var model = new InterfaceMethodModel
        {
            Id = 1,
            Name = "UpdatedMethod",
            ReturnType = "string"
        };

        var result = await _controller.Update(model);

        _mockService.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }
}