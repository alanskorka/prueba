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
}
