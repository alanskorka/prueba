using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
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
}
