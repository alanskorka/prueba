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
}