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
}
