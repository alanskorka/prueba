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
}
