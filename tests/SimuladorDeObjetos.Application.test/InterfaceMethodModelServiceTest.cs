using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.Test;

[TestClass]
public class InterfaceMethodModelServiceTest
{
    private Mock<IInterfaceMethodModelRepository> _mockRepository = null!;
    private InterfaceMethodModelService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IInterfaceMethodModelRepository>();
        _service = new InterfaceMethodModelService(_mockRepository.Object);
    }

    [TestMethod]
    public async Task Add_ShouldCallRepository()
    {
        var model = new InterfaceMethodModel
        {
            Name = "TestMethod",
            ReturnType = "void"
        };

        await _service.Add(model);

        _mockRepository.Verify(r => r.Add(model), Times.Once);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllMethodsFromRepository()
    {
        var expected = new List<InterfaceMethodModel>
        {
            new InterfaceMethodModel
            {
                Name = "Method1",
                ReturnType = "void"
            },
            new InterfaceMethodModel
            {
                Name = "Method2",
                ReturnType = "string"
            }
        };

        _mockRepository.Setup(r => r.GetAll())
            .ReturnsAsync(expected);

        var result = await _service.GetAll();

        _mockRepository.Verify(r => r.GetAll(), Times.Once);
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());

        var methodsList = result.ToList();
        Assert.AreEqual("Method1", methodsList[0].Name);
        Assert.AreEqual("Method2", methodsList[1].Name);
    }

    [TestMethod]
    public async Task Update_ShouldCallRepository()
    {
        var model = new InterfaceMethodModel
        {
            Id = 1,
            Name = "UpdatedMethod",
            ReturnType = "string"
        };

        await _service.Update(model);

        _mockRepository.Verify(r => r.Update(model), Times.Once);
    }

    [TestMethod]
    public async Task Delete_ShouldCallRepository()
    {
        var id = 1;

        await _service.Delete(id);

        _mockRepository.Verify(r => r.Delete(id), Times.Once);
    }
}
