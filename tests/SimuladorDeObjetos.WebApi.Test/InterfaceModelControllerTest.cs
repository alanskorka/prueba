using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Test;

[TestClass]
public class InterfaceModelControllerTest
{
    private Mock<IInterfaceModelService> _serviceMock = null!;
    private InterfaceModelController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IInterfaceModelService>();
        _controller = new InterfaceModelController(_serviceMock.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnListOfInterfaces()
    {
        var interfaces = new List<InterfaceModel> { new() { Id = 1, Name = "I1" } };
        _serviceMock.Setup(s => s.GetAll()).ReturnsAsync(interfaces);
        var result = await _controller.GetAll();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("I1", result.First().Name);
    }
}
