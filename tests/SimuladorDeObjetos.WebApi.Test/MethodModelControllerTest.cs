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
}
