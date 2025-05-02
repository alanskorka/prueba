using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class AttributeModelServiceTest
{
    private Mock<IAtributteModelRepository>? _mockRepo;
    private AttributeModelService? _service;
    private AttributeModel? _attribute;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IAtributteModelRepository>();
        _service = new AttributeModelService(_mockRepo.Object);
        _attribute = new AttributeModel
        {
            Id = Guid.NewGuid(),
            Name = "Attr",
            Type = "string",
            ClassId = Guid.NewGuid(),
            Accessibility = Domain.Enums.AccessibilityModifier.Public
        };
    }

    [TestMethod]
    public void Create_ShouldCallRepositoryAdd()
    {
        _service.Create(_attribute ?? throw new InvalidOperationException());
        _mockRepo.Verify(r => r.Add(_attribute), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Create_ShouldThrow_WhenAttributeIsNull()
    {
        _service.Create(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnListFromRepository()
    {
        var list = new List<AttributeModel> { _attribute ?? throw new InvalidOperationException() };
        _mockRepo.Setup(r => r.GetAll()).Returns(list);

        var result = _service.GetAll();

        CollectionAssert.AreEqual(list, result);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdate()
    {
        _service.Update(_attribute ?? throw new InvalidOperationException());
        _mockRepo.Verify(r => r.Update(_attribute), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenAttributeIsNull()
    {
        _service.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete()
    {
        _service.Delete(_attribute ?? throw new InvalidOperationException());
        _mockRepo.Verify(r => r.Delete(_attribute), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenAttributeIsNull()
    {
        _service.Delete(null!);
    }
}
