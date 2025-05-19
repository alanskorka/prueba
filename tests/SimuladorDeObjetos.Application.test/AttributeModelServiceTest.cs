using Domain.Entities;
using Domain.Enums;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class AttributeModelServiceTest
{
    private Mock<IAtributteModelRepository>? _mockRepo;
    private Mock<IClassModelRepository>? _mockClassRepo;
    private AttributeModelService? _service;
    private AttributeModel? _attribute;
    private ClassModel? _classModel;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IAtributteModelRepository>();
        _mockClassRepo = new Mock<IClassModelRepository>();

        _service = new AttributeModelService(_mockRepo.Object, _mockClassRepo.Object);

        _classModel = new ClassModel
        {
            Id = Guid.NewGuid(),
            Name = "TestClass",
            IsAbstract = false,
            IsSealed = false,
            Attributes = new List<AttributeModel>(),
            Methods = new List<MethodModel>()
        };

        _attribute = new AttributeModel
        {
            Id = Guid.NewGuid(),
            Name = "Attr",
            Type = "string",
            ClassId = _classModel.Id,
            Accessibility = AccessibilityModifier.Public
        };
    }

    [TestMethod]
    public void Create_ShouldCallRepositoryAdd_WhenValid()
    {
        _mockClassRepo!.Setup(r => r.GetById(_classModel!.Id)).Returns(_classModel);
        _service!.Create(_attribute!);
        _mockRepo!.Verify(r => r.Add(_attribute!), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Create_ShouldThrow_WhenAttributeIsNull()
    {
        _service!.Create(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Create_ShouldThrow_WhenClassNotFound()
    {
        _mockClassRepo!.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ClassModel?)null);
        _service!.Create(_attribute!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Create_ShouldThrow_WhenClassIsSealed()
    {
        _classModel!.IsSealed = true;
        _mockClassRepo!.Setup(r => r.GetById(_classModel.Id)).Returns(_classModel);
        _service!.Create(_attribute!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Create_ShouldThrow_WhenAttributeNameExists()
    {
        _classModel!.Attributes.Add(new AttributeModel { Name = _attribute!.Name });
        _mockClassRepo!.Setup(r => r.GetById(_classModel.Id)).Returns(_classModel);
        _service!.Create(_attribute!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnListFromRepository()
    {
        var list = new List<AttributeModel> { _attribute! };
        _mockRepo!.Setup(r => r.GetAll()).Returns(list);

        var result = _service!.GetAll();

        CollectionAssert.AreEqual(list, result);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdate()
    {
        _service!.Update(_attribute!);
        _mockRepo!.Verify(r => r.Update(_attribute!), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenAttributeIsNull()
    {
        _service!.Update(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenAttributeNotFound()
    {
        _mockRepo!.Setup(r => r.GetById(_attribute!.Id)).Returns((AttributeModel?)null);
        _service!.Update(_attribute!);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete()
    {
        _service!.Delete(_attribute!);
        _mockRepo!.Verify(r => r.Delete(_attribute!), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenAttributeIsNull()
    {
        _service!.Delete(null!);
    }
}
