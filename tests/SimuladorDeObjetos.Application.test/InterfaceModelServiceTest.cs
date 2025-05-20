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
                }
            }
        };
        await _service.Add(interfaceModel);
        _repository.Verify(r => r.Add(interfaceModel), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task AddInterface_WithDuplicateMethodNames_ShouldThrow()
    {
        var model = new InterfaceModel
        {
            Name = "IDuplicated",
            Methods = new List<InterfaceMethodModel>
            {
                new() { Name = "DoSomething", ReturnType = "void" },
                new() { Name = "DoSomething", ReturnType = "void" }
            }
        };

        await _service.Add(model);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllInterfaces()
    {
        var expected = new List<InterfaceModel>
        {
            new InterfaceModel { Name = "ITest1" },
            new InterfaceModel { Name = "ITest2" }
        };

        _repository.Setup(r => r.GetAll()).ReturnsAsync(expected);

        var result = await _service.GetAll();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("ITest1", result[0].Name);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnCorrectInterface()
    {
        var interfaceId = 1;
        var expected = new InterfaceModel { Id = interfaceId, Name = "ITest" };

        _repository.Setup(r => r.GetById(interfaceId)).ReturnsAsync(expected);

        var result = await _service.GetById(interfaceId);

        Assert.IsNotNull(result);
        Assert.AreEqual("ITest", result.Name);
    }

    [TestMethod]
    public async Task Delete_WhenNotUsedByAnyClass_ShouldCallDelete()
    {
        var id = 1;
        _repository.Setup(r => r.IsUsedByAnyClass(id)).ReturnsAsync(false);

        await _service.Delete(id);
        _repository.Verify(r => r.Delete(id), Times.Once);
    }

    [TestMethod]
    public async Task Update_ShouldCallRepositoryUpdate()
    {
        var model = new InterfaceModel
        {
            Id = 1,
            Name = "IModified",
            Methods = new List<InterfaceMethodModel>
            {
                new() { Name = "DoX", ReturnType = "string", Parameters = new() }
            }
        };

        await _service.Update(model);

        _repository.Verify(r => r.Update(model), Times.Once);
    }
}
