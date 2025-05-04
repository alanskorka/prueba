using AutoMapper;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class MethodCallModelControllerTest
{
    private Mock<IMethodCallModelService>? _mockService;
    private Mock<IMapper>? _mockMapper;
    private MethodCallModelController? _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IMethodCallModelService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new MethodCallModelController(_mockService.Object, _mockMapper.Object);
    }
}
