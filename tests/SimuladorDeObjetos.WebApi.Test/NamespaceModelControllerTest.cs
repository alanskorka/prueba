using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class NamespaceControllerTest
{
    private Mock<INamespaceService> _mockService = null!;
    private NamespacesController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<INamespaceService>();
        _controller = new NamespacesController(_mockService.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllNamespaces()
    {
        var expected = new List<NamespaceModel>
        {
            new NamespaceModel { Name = "NS1" },
            new NamespaceModel { Name = "NS2" }
        };

        _mockService.Setup(s => s.GetAll()).Returns(expected);
        var result = _controller.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<NamespaceModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var model = new NamespaceModel { Name = "NewNS" };
        var result = _controller.Add(model);

        _mockService.Verify(s => s.Create(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var model = new NamespaceModel { Name = "UpdatedNS" };
        var result = _controller.Update(model);

        _mockService.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var model = new NamespaceModel { Name = "ToDelete" };
        var result = _controller.Delete(model);

        _mockService.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Add_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var model = new NamespaceModel();

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var model = new NamespaceModel();

        var result = _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void GetAll_ShouldReturnProblem_WhenExceptionThrown()
    {
        _mockService.Setup(s => s.GetAll()).Throws(new Exception("error"));

        var result = _controller.GetAll();

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Add_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new NamespaceModel { Name = "NS" };
        _mockService.Setup(s => s.Create(model)).Throws(new Exception("error"));

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new NamespaceModel { Name = "NS" };
        _mockService.Setup(s => s.Update(model)).Throws(new Exception("error"));

        var result = _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrows()
    {
        var model = new NamespaceModel { Name = "NS" };
        _mockService.Setup(s => s.Delete(model)).Throws(new Exception("error"));

        var result = _controller.Delete(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
