using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.DTOs.Api;
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

    [TestMethod]
    public void GetAll_ShouldReturnAllMethods()
    {
        var expected = new List<MethodModel>
        {
            new MethodModel { Name = "Method1" },
            new MethodModel { Name = "Method2" }
        };

        _mockService!.Setup(s => s.GetAll()).Returns(expected);

        var result = _controller!.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<MethodModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var method = new MethodModel { Name = "NewMethod" };

        var result = _controller!.Add(method);

        _mockService!.Verify(s => s.Add(method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var method = new MethodModel { Name = "UpdatedMethod" };

        var result = _controller!.Update(method);

        _mockService!.Verify(s => s.Update(method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var method = new MethodModel { Name = "ToDelete" };

        var result = _controller!.Delete(method);

        _mockService!.Verify(s => s.Delete(method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void AddToClass_ShouldCallServiceWithCorrectParameters_andReturnOk()
    {
        var classId = Guid.NewGuid();
        var method = new MethodModel { Id = Guid.NewGuid(), Name = "NewInClass" };
        var result = _controller!.AddToClass(classId, method);
        _mockService!.Verify(s => s.AddMethodToClass(classId, method), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Simulate_ShouldReturnOkWithSimulationResponse()
    {
        var req = new SimulationRequest
        {
            ReferenceTypeId = Guid.NewGuid(),
            ConcreteTypeId = Guid.NewGuid(),
            MethodId = Guid.NewGuid()
        };
        var expectedResponse = new SimulationResponse
        {
            Lines = new List<string>
            {
                "SomeClass.SomeMethod()",
                "  this.SubCall()"
            }
        };
        _mockService!
            .Setup(s => s.SimulateMethodExecution(req))
            .Returns(expectedResponse);

        var actionResult = _controller!.Simulate(req) as OkObjectResult;

        Assert.IsNotNull(actionResult, "Debe devolver OkObjectResult");
        Assert.AreSame(expectedResponse, actionResult!.Value, "El valor devuelto debe ser el SimulationResponse del servicio");
        _mockService.Verify(s => s.SimulateMethodExecution(req), Times.Once);
    }

    [TestMethod]
    public void Add_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var method = new MethodModel();

        var result = _controller.Add(method);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var method = new MethodModel();

        var result = _controller.Update(method);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Add_ShouldReturnProblem_WhenServiceThrows()
    {
        var method = new MethodModel { Name = "fail" };
        _mockService!.Setup(s => s.Add(method)).Throws(new Exception("fail"));

        var result = _controller!.Add(method);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrows()
    {
        var method = new MethodModel { Name = "fail" };
        _mockService!.Setup(s => s.Update(method)).Throws(new Exception("fail"));

        var result = _controller!.Update(method);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrows()
    {
        var method = new MethodModel { Name = "fail" };
        _mockService!.Setup(s => s.Delete(method)).Throws(new Exception("fail"));

        var result = _controller!.Delete(method);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void AddToClass_ShouldReturnProblem_WhenServiceThrows()
    {
        var method = new MethodModel { Name = "fail" };
        var classId = Guid.NewGuid();
        _mockService!.Setup(s => s.AddMethodToClass(classId, method)).Throws(new Exception("fail"));

        var result = _controller!.AddToClass(classId, method);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Simulate_ShouldReturnProblem_WhenServiceThrows()
    {
        var req = new SimulationRequest
        {
            ReferenceTypeId = Guid.NewGuid(),
            ConcreteTypeId = Guid.NewGuid(),
            MethodId = Guid.NewGuid()
        };

        _mockService!.Setup(s => s.SimulateMethodExecution(req)).Throws(new Exception("fail"));

        var result = _controller!.Simulate(req);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void GetAll_ShouldReturnProblem_WhenServiceThrows()
    {
        _mockService!.Setup(s => s.GetAll()).Throws(new Exception("fail"));

        var result = _controller!.GetAll();

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void AddToClass_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var method = new MethodModel();
        var classId = Guid.NewGuid();

        var result = _controller!.AddToClass(classId, method);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Simulate_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        _controller!.ModelState.AddModelError("MethodId", "Required");

        var req = new SimulationRequest();

        var result = _controller.Simulate(req);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}
