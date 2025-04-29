using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class ClassModelServiceTest
{
    private Mock<IClassModelRepository>? _mockRepository;
    private ClassModelService? _service;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepository = new Mock<IClassModelRepository>();
        _service = new ClassModelService(_mockRepository.Object);
    }

    [TestMethod]
    public void Add_ShouldCallRepositoryAdd()
    {
        var newModel = new ClassModel { Name = "NewClass" };

        _service.Add(newModel);

        _mockRepository.Verify(r => r.Add(newModel), Times.Once);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllClassModels()
    {
        var expectedModels = new List<ClassModel>
        {
            new ClassModel { Name = "Class1" },
            new ClassModel { Name = "Class2" }
        };

        _mockRepository.Setup(r => r.GetAll()).Returns(expectedModels);

        var result = _service.GetAll();

        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Class1", result.First().Name);
    }
}
