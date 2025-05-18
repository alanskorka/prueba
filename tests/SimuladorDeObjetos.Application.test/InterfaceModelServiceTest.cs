using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class InterfaceModelServiceTest
{
    private InterfaceModelService? _service;
    private Mock<IInterfaceModelRepository>? _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = new Mock<IInterfaceModelRepository>();
        _service = new InterfaceModelService(_repository.Object);
    }

    [TestMethod]
    public async Task AddInterface_WithValidMethodSignatures_ShouldCallRepository()
    {
        var interfaceModel = new InterfaceModel
        {
            Name = "IMyInterface",
            Methods = new List<InterfaceMethodModel>
            {
                new InterfaceMethodModel
                {
                    Name = "DoSomething",
                    ReturnType = "void",
                    Parameters = new List<ParameterModel>
                    {
                        new ParameterModel { Name = "value", Type = "int" }
                    }
                }
            }
        };
        await _service.Add(interfaceModel);
        _repository.Verify(r => r.Add(interfaceModel), Times.Once);
    }
}
