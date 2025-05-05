using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class AttributeModelControllerTest
{
    private Mock<IAttributeModelService>? _mockService;
    private AttributeModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IAttributeModelService>();
        _controller = new AttributeModelController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllAttributes()
    {
        var expected = new List<AttributeModel>
        {
            new AttributeModel { Name = "Attr1" },
            new AttributeModel { Name = "Attr2" }
        };

        _mockService!.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller!.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<AttributeModel>;
        Assert.AreEqual(2, returned!.Count());
    }
}
