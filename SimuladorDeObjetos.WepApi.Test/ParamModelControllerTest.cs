using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;
namespace SimuladorDeObjetos.WepApi.Test;

[TestClass]
public class ParamModelControllerTest
{
    private Mock<IParamModelService>? _mockService;
    private ParamModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IParamModelService>();
        _controller = new ParamModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllParams()
    {
        var expected = new List<ParamModel>
        {
            new ParamModel { Name = "param1", Type = "string" },
            new ParamModel { Name = "param2", Type = "int" }
        };
        _mockService.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<ParamModel>;
        Assert.IsNotNull(returned);
        Assert.AreEqual(2, returned.Count());
    }
}
