using Domain.Entities;
using Domain.Enums;
using Moq;
using SimuladorDeObjetos.Application.DTOs.Api;
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

    [TestMethod]
    public void GetByName_ReturnsMethodModel_WhenClassIdAndMethodNameMatch()
    {
        var classId = Guid.NewGuid();
        var methodName = "MyMethod";
        var methodId = Guid.NewGuid();

        var fakeMethods = new List<MethodModel>
        {
            new MethodModel { Id = methodId, ClassId = classId, Name = methodName },
            new MethodModel { Id = Guid.NewGuid(), ClassId = classId, Name = "OtherMethod" }
        };

        var methodRepoMock = new Mock<IMethodModelRepository>();
        var classRepoMock = new Mock<IClassModelRepository>();

        methodRepoMock.Setup(r => r.GetAll()).Returns(fakeMethods);

        var service = new MethodModelService(methodRepoMock.Object, classRepoMock.Object);

        var result = service.GetByName(classId, methodName);

        Assert.IsNotNull(result);
        Assert.AreEqual(methodId, result.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SimulateMethodExecution_ShouldThrow_WhenMethodNotFound()
    {
        _methodRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((MethodModel?)null);

        var req = new SimulationRequest
        {
            ReferenceTypeId = Guid.NewGuid(),
            ConcreteTypeId = Guid.NewGuid(),
            MethodId = Guid.NewGuid()
        };

        _service.SimulateMethodExecution(req);
    }

    [TestMethod]
        public void SimulateMethodExecution_ShouldReturnOnlyInitialLine_IfNoCalls()
        {
            var methodId = Guid.NewGuid();
            var method = new MethodModel { Id = methodId, Name = "DoWork" };

            _methodRepo.Setup(r => r.GetById(methodId)).Returns(method);
            _methodRepo.Setup(r => r.GetMethodCalls(methodId)).Returns(new List<MethodCallModel>());

            var req = new SimulationRequest
            {
                ReferenceTypeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ConcreteTypeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                MethodId = methodId
            };

            var resp = _service.SimulateMethodExecution(req);

            Assert.AreEqual(1, resp.Lines.Count);
            Assert.AreEqual(
                "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb.DoWork()",
                resp.Lines[0]);
        }

        [TestMethod]
        public void SimulateMethodExecution_ShouldIndentCalls_Correctly()
        {
            var methodId = Guid.NewGuid();
            var method = new MethodModel { Id = methodId, Name = "Main" };

            var call1 = new MethodCallModel
            {
                Id = Guid.NewGuid(),
                MethodName = "Step1",
                ReferenceType = ReferenceTypeInvocation.This,
                ReferenceName = null
            };
            var call2 = new MethodCallModel
            {
                Id = Guid.NewGuid(),
                MethodName = "Step2",
                ReferenceType = ReferenceTypeInvocation.Attribute,
                ReferenceName = "repo"
            };
            var nested = new MethodCallModel
            {
                Id = Guid.NewGuid(),
                MethodName = "Nested",
                ReferenceType = ReferenceTypeInvocation.Base,
                ReferenceName = null
            };

            _methodRepo.Setup(r => r.GetById(methodId)).Returns(method);
            _methodRepo.Setup(r => r.GetMethodCalls(methodId))
                       .Returns(new List<MethodCallModel> { call1, call2 });
            _methodRepo.Setup(r => r.GetMethodCalls(call1.Id)).Returns(new List<MethodCallModel>());
            _methodRepo.Setup(r => r.GetMethodCalls(call2.Id)).Returns(new List<MethodCallModel> { nested });
            _methodRepo.Setup(r => r.GetMethodCalls(nested.Id)).Returns(new List<MethodCallModel>());

            var req = new SimulationRequest
            {
                ReferenceTypeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ConcreteTypeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                MethodId = methodId
            };

            var resp = _service.SimulateMethodExecution(req);
            var expected = new[]
            {
                "22222222-2222-2222-2222-222222222222.Main()",
                "  this.Step1()",
                "  obj_repo.Step2()",
                "    base.Nested()"
            };
            CollectionAssert.AreEqual(expected, resp.Lines);
        }
}
