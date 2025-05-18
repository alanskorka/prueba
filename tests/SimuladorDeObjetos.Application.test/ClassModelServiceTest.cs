using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class ClassModelServiceTest
{
    private Mock<IClassModelRepository>? _mockRepo;
    private ClassModelService? _service;
    private ClassModel? _class;
    private ClassModel? _baseClass;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepo = new Mock<IClassModelRepository>();
        _service = new ClassModelService(_mockRepo.Object);

        _class = new ClassModel
        {
            Id = Guid.NewGuid(),
            Name = "TestClass",
            IsSealed = false,
            IsAbstract = false
        };

        _baseClass = new ClassModel
        {
            Id = Guid.NewGuid(),
            Name = "BaseClass",
            IsSealed = false,
            IsAbstract = false
        };
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllClassModels()
    {
        var expected = new List<ClassModel>
        {
            new ClassModel { Name = "Class1" },
            new ClassModel { Name = "Class2" }
        };

        _mockRepo!.Setup(r => r.GetAll()).Returns(expected);

        var result = _service!.GetAll();

        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Class1", result.First().Name);
    }

    [TestMethod]
    public void Add_ShouldAdd_WhenNoBaseClass()
    {
        var model = new ClassModel { Name = "C" };
        _service.Add(model);
        _mockRepo.Verify(r => r.Add(model), Times.Once);
    }

    [TestMethod]
    public void Add_ShouldAdd_WhenBaseClassValid()
    {
        var baseId = Guid.NewGuid();
        var baseClass = new ClassModel { Id = baseId, IsSealed = false };
        var model = new ClassModel { Name = "C", BaseClassId = baseId };

        _mockRepo.Setup(r => r.GetById(baseId)).Returns(baseClass);

        _service.Add(model);

        _mockRepo.Verify(r => r.Add(model), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenBaseClassNotFound()
    {
        var model = new ClassModel { Name = "C", BaseClassId = Guid.NewGuid() };
        _mockRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ClassModel?)null);

        _service.Add(model);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenBaseClassIsSealed()
    {
        var baseId = Guid.NewGuid();
        var model = new ClassModel { Name = "C", BaseClassId = baseId };
        var baseClass = new ClassModel { Id = baseId, IsSealed = true };

        _mockRepo.Setup(r => r.GetById(baseId)).Returns(baseClass);

        _service.Add(model);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete()
    {
        _service!.Delete(_class!);
        _mockRepo!.Verify(r => r.Delete(_class!), Times.Once);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdateAndSave()
    {
        _service!.Update(_class!);

        _mockRepo!.Verify(r => r.Update(_class!), Times.Once);
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void SaveChanges_ShouldCallRepositorySaveChanges()
    {
        _service!.SaveChanges();
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void GetByName_ReturnsClassModel_WhenNameExists()
    {
        var className = _class!.Name;
        var list = new List<ClassModel> { _class!, new ClassModel { Name = "Other" } };

        _mockRepo!.Setup(r => r.GetAll()).Returns(list);
        var result = _service!.GetByName(className ?? throw new InvalidOperationException());

        Assert.IsNotNull(result);
        Assert.AreEqual(_class.Id, result!.Id);
    }

    [TestMethod]
    public void GetByName_ReturnsNull_WhenNameDoesNotExist()
    {
        var list = new List<ClassModel> { new ClassModel { Name = "Other" } };
        _mockRepo!.Setup(r => r.GetAll()).Returns(list);

        var result = _service!.GetByName("NotFound");
        Assert.IsNull(result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))] 
    public async Task AddClass_ThatDoesNotImplementAllInterfaceMethods_ShouldThrow()
    {
        var interfaceModel = new InterfaceModel
        {
            Name = "IMyInterface",
            Methods = new List<InterfaceMethodModel>
            {
                new() { Name = "DoIt", ReturnType = "void", Parameters = new() }
            }
        };

        var classModel = new ClassModel
        {
            Name = "MyClass",
            ImplementedInterfaces = new List<InterfaceModel> { interfaceModel },
            Methods = new List<MethodModel>() // missing DoIt()
        };

        await _service.Add(classModel);
    }
}
