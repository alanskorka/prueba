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
}
