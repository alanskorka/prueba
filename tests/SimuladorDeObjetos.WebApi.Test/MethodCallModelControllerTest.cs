using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.DTOs;
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

    [TestMethod]
    public void GetAll_ReturnsOkResultWithDtos()
    {
        var models = new List<MethodCallModel>
        {
            new MethodCallModel { MethodName = "M1", ReferenceType = ReferenceTypeInvocation.This }
        };
        var dtos = new List<MethodCallDto>
        {
            new MethodCallDto { MethodName = "M1", ReferenceType = ReferenceTypeInvocation.This }
        };
        _mockService.Setup(s => s.GetAll()).Returns(models);
        _mockMapper.Setup(m => m.Map<List<MethodCallDto>>(models)).Returns(dtos);

        var result = _controller.GetAll();

        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        CollectionAssert.AreEqual(dtos, (List<MethodCallDto>)ok.Value);
    }
}
