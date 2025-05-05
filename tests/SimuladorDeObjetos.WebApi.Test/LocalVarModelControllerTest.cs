using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class LocalVarModelControllerTest
{
    private Mock<ILocalVarModelService>? _mockService;
    private LocalVarModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<ILocalVarModelService>();
        _controller = new LocalVarModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllLocalVars()
    {
        var expected = new List<LocalVarModel>
        {
            new LocalVarModel { Name = "Var1" },
            new LocalVarModel { Name = "Var2" }
        };

        _mockService!.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller!.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<LocalVarModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var model = new LocalVarModel { Name = "NewVar" };

        var result = _controller!.Add(model);

        _mockService!.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var model = new LocalVarModel { Name = "UpdatedVar" };

        var result = _controller!.Update(model);

        _mockService!.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var model = new LocalVarModel { Name = "DeleteVar" };

        var result = _controller!.Delete(model);

        _mockService!.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}
