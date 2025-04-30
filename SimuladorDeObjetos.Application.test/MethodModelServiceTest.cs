using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MethodModelServiceTest
{
    private Mock<IMethodModelRepository>? _methodRepo;
    private Mock<IClassModelRepository>? _classRepo;
    private MethodModelService? _service;

    [TestInitialize]
    public void Init()
    {
        _methodRepo = new Mock<IMethodModelRepository>();
        _classRepo = new Mock<IClassModelRepository>();
        _service = new MethodModelService(_methodRepo.Object, _classRepo.Object);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenClassNotFound()
    {
        _classRepo!.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ClassModel?)null);

        var ex = Assert.ThrowsException<Exception>(() =>
            _service!.AddMethodToClass(Guid.NewGuid(), new MethodModel()));

        Assert.AreEqual("Class not found", ex.Message);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenClassIdIsEmpty()
    {
        var method = new MethodModel { Name = "MetodoInvalido" };

        var ex = Assert.ThrowsException<ArgumentException>(() =>
            _service!.AddMethodToClass(Guid.Empty, method));

        Assert.AreEqual("classId is empty", ex.Message);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenMethodIsNull()
    {
        var classId = Guid.NewGuid();

        Assert.ThrowsException<ArgumentNullException>(() =>
            _service!.AddMethodToClass(classId, null!));
    }

    [TestMethod]
    public void AddMethodToClass_ShouldAddMethod_WhenClassExists()
    {
        var classId = Guid.NewGuid();
        var classModel = new ClassModel { Id = classId, Methods = new List<MethodModel>() };
        var newMethod = new MethodModel { Name = "NuevoMetodo" };

        _classRepo!.Setup(r => r.GetById(classId)).Returns(classModel);

        _service!.AddMethodToClass(classId, newMethod);

        Assert.IsTrue(classModel.Methods.Contains(newMethod));
        _classRepo.Verify(r => r.Update(classModel), Times.Once);
        _classRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldAddMethod_WhenClassExists_EmptyMethodsList()
    {
        var classId = Guid.NewGuid();
        var classModel = new ClassModel { Id = classId, Methods = new List<MethodModel>() };
        var newMethod = new MethodModel { Name = "NuevoMetodo" };

        _classRepo!.Setup(r => r.GetById(classId)).Returns(classModel);

        _service!.AddMethodToClass(classId, newMethod);

        Assert.IsTrue(classModel.Methods.Contains(newMethod));
        _classRepo.Verify(r => r.Update(classModel), Times.Once);
        _classRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllMethods()
    {
        var expectedMethods = new List<MethodModel>
        {
            new MethodModel { Name = "Test1" },
            new MethodModel { Name = "Test2" }
        };

        _methodRepo!.Setup(r => r.GetAll()).Returns(expectedMethods);

        var result = _service!.GetAll().ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Test1", result[0].Name);
        Assert.AreEqual("Test2", result[1].Name);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        var method = new MethodModel { Name = "Test" };

        _service!.Add(method);

        _methodRepo!.Verify(r => r.Add(method), Times.Once);
        _methodRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Update_ShouldCallUpdateAndSaveChanges()
    {
        var method = new MethodModel { Name = "UpdatedMethod" };

        _service!.Update(method);

        _methodRepo!.Verify(r => r.Update(method), Times.Once);
        _methodRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Delete_ShouldCallDeleteAndSaveChanges()
    {
        var method = new MethodModel { Name = "MethodToDelete" };

        _service!.Delete(method);

        _methodRepo!.Verify(r => r.Delete(method), Times.Once);
        _methodRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }
}
