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

    [TestMethod]
    public void GetById_ShouldReturnLocalVar_WhenExists()
    {
        var expected = new LocalVarModel { Id = Guid.NewGuid(), Name = "Var" };
        _mockService!.Setup(s => s.GetById(expected.Id)).Returns(expected);

        var result = _controller!.GetById(expected.Id) as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as LocalVarModel;
        Assert.IsNotNull(returned);
        Assert.AreEqual(expected.Id, returned.Id);
    }

    [TestMethod]
    public void GetById_ShouldReturnNotFound_WhenNotExists()
    {
        var id = Guid.NewGuid();
        _mockService!.Setup(s => s.GetById(id)).Returns((LocalVarModel?)null);

        var result = _controller!.GetById(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var model = new LocalVarModel { Name = "NewVar" };

        var result = _controller!.Add(model);

        _mockService!.Verify(s => s.Add(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var id = Guid.NewGuid();
        var model = new LocalVarModel { Id = id, Name = "UpdatedVar" };

        var result = _controller!.Update(id, model);

        _mockService!.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var id = Guid.NewGuid();
        var model = new LocalVarModel { Id = id, Name = "DeleteVar" };
        _mockService!.Setup(s => s.GetById(id)).Returns(model);

        var result = _controller!.Delete(id);

        _mockService!.Verify(s => s.GetById(id), Times.Once);
        _mockService!.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnNotFound_WhenNotExists()
    {
        var id = Guid.NewGuid();
        _mockService!.Setup(s => s.GetById(id)).Returns((LocalVarModel?)null);

        var result = _controller!.Delete(id);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public void Add_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new LocalVarModel();

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller!.ModelState.AddModelError("Name", "Required");
        var model = new LocalVarModel();

        var result = _controller.Update(Guid.NewGuid(), model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
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
    public void Add_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new LocalVarModel { Name = "X" };
        _mockService!.Setup(s => s.Add(model)).Throws(new Exception("fail"));

        var result = _controller!.Add(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrows()
    {
        var id = Guid.NewGuid();
        var model = new LocalVarModel { Id = id, Name = "X" };
        _mockService!.Setup(s => s.GetById(id)).Returns(model);
        _mockService!.Setup(s => s.Delete(model)).Throws(new Exception("fail"));

        var result = _controller!.Delete(id);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrows()
    {
        var id = Guid.NewGuid();
        var model = new LocalVarModel { Id = id, Name = "X" };
        _mockService!.Setup(s => s.Update(model)).Throws(new Exception("fail"));

        var result = _controller!.Update(id, model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
