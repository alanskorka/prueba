using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Application.Services.Interfaces;
using SimuladorDeObjetos.WebApi.Controllers;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class AttributeModelControllerTest
{
    private Mock<IAttributeModelService> _mockService = null!;
    private AttributeModelController _controller = null!;

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

        _mockService.Setup(s => s.GetAll()).Returns(expected);
        var result = _controller.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        var returned = result.Value as IEnumerable<AttributeModel>;
        Assert.AreEqual(2, returned!.Count());
    }

    [TestMethod]
    public void Add_ShouldReturnOk()
    {
        var model = new AttributeModel { Name = "NewAttr" };
        var result = _controller.Add(model);

        _mockService.Verify(s => s.Create(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Update_ShouldReturnOk()
    {
        var model = new AttributeModel { Name = "UpdatedAttr" };
        var result = _controller.Update(model);

        _mockService.Verify(s => s.Update(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Delete_ShouldReturnOk()
    {
        var model = new AttributeModel { Name = "AttrToDelete" };
        var result = _controller.Delete(model);

        _mockService.Verify(s => s.Delete(model), Times.Once);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public void Add_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var model = new AttributeModel();

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void Update_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var model = new AttributeModel();

        var result = _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void GetAll_ShouldReturnProblem_WhenExceptionThrown()
    {
        _mockService.Setup(s => s.GetAll()).Throws(new Exception("Test Error"));

        var result = _controller.GetAll();

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        var problem = result as ObjectResult;
        Assert.AreEqual(500, problem!.StatusCode);
    }

    [TestMethod]
    public void Add_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new AttributeModel { Name = "Attr" };
        _mockService.Setup(s => s.Create(model)).Throws(new Exception("fail"));

        var result = _controller.Add(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Update_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new AttributeModel { Name = "Attr" };
        _mockService.Setup(s => s.Update(model)).Throws(new Exception("fail"));

        var result = _controller.Update(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }

    [TestMethod]
    public void Delete_ShouldReturnProblem_WhenServiceThrowsException()
    {
        var model = new AttributeModel { Name = "Attr" };
        _mockService.Setup(s => s.Delete(model)).Throws(new Exception("fail"));

        var result = _controller.Delete(model);

        Assert.IsInstanceOfType(result, typeof(ObjectResult));
        Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
    }
}
