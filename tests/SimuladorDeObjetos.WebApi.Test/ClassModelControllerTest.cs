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
}
