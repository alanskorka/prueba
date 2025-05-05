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
}
