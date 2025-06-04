using Domain.Entities;
using Domain.Enums;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Application.DTOs.Api;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MethodModelServiceTest
{
    private Mock<IMethodModelRepository> _methodRepo = null!;
    private Mock<IClassModelRepository> _classRepo = null!;
    private MethodModelService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _methodRepo = new Mock<IMethodModelRepository>();
        _classRepo = new Mock<IClassModelRepository>();
        _service = new MethodModelService(_methodRepo.Object, _classRepo.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnEmpty_WhenNull()
    {
        _methodRepo.Setup(r => r.GetAll()).Returns((List<MethodModel>?)null);
        var result = _service.GetAll();
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public void Add_ShouldAddMethod_WhenValid()
    {
        var method = new MethodModel { Name = "M", ClassId = Guid.NewGuid() };
        var cls = new ClassModel { Id = method.ClassId, Methods = new(), IsSealed = false, IsAbstract = true };
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns(cls);

        _service.Add(method);

        _methodRepo.Verify(r => r.Add(method), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenClassNotFound()
    {
        _classRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ClassModel?)null);
        _service.Add(new MethodModel { ClassId = Guid.NewGuid() });
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenClassIsSealed()
    {
        var method = new MethodModel { Name = "X", ClassId = Guid.NewGuid() };
        var cls = new ClassModel { Id = method.ClassId, IsSealed = true, Methods = new() };
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns(cls);
        _service.Add(method);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenDuplicateMethod()
    {
        var method = new MethodModel { Name = "X", ClassId = Guid.NewGuid() };
        var cls = new ClassModel { Id = method.ClassId, Methods = new() { new MethodModel { Name = "X" } } };
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns(cls);
        _service.Add(method);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenAbstractMethodInConcreteClass()
    {
        var method = new MethodModel { Name = "X", ClassId = Guid.NewGuid(), IsAbstract = true };
        var cls = new ClassModel { Id = method.ClassId, IsAbstract = false, Methods = new() };
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns(cls);
        _service.Add(method);
    }

    [TestMethod]
    public void Update_ShouldSucceed_WhenValid()
    {
        var method = new MethodModel { Id = Guid.NewGuid(), ClassId = Guid.NewGuid(), Name = "Valid" };
        var classModel = new ClassModel
        {
            Id = method.ClassId,
            Methods = new List<MethodModel>
            {
                new MethodModel { Id = Guid.NewGuid(), Name = "Other" }
            }
        };

        _methodRepo.Setup(r => r.GetById(method.Id)).Returns(method);
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns(classModel);

        _service.Update(method);

        _methodRepo.Verify(r => r.Update(method), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenMethodIsNull()
    {
        _service.Update(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenMethodNotFound()
    {
        var method = new MethodModel { Id = Guid.NewGuid() };
        _methodRepo.Setup(r => r.GetById(method.Id)).Returns((MethodModel?)null);

        _service.Update(method);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenClassNotFound()
    {
        var method = new MethodModel { Id = Guid.NewGuid(), ClassId = Guid.NewGuid(), Name = "Run" };

        _methodRepo.Setup(r => r.GetById(method.Id)).Returns(method);
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns((ClassModel?)null);

        _service.Update(method);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenDuplicateMethodNameInClass()
    {
        var method = new MethodModel { Id = Guid.NewGuid(), ClassId = Guid.NewGuid(), Name = "Run" };
        var classModel = new ClassModel
        {
            Id = method.ClassId,
            Methods = new List<MethodModel>
            {
                new MethodModel { Id = Guid.NewGuid(), Name = "Run" }
            }
        };

        _methodRepo.Setup(r => r.GetById(method.Id)).Returns(method);
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns(classModel);

        _service.Update(method);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete_WhenMethodExists()
    {
        var method = new MethodModel { Id = Guid.NewGuid() };
        _methodRepo.Setup(r => r.GetById(method.Id)).Returns(method);

        _service.Delete(method);

        _methodRepo.Verify(r => r.Delete(method), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenMethodIsNull()
    {
        _service.Delete(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Delete_ShouldThrow_WhenMethodDoesNotExist()
    {
        var method = new MethodModel { Id = Guid.NewGuid() };
        _methodRepo.Setup(r => r.GetById(method.Id)).Returns((MethodModel?)null);

        _service.Delete(method);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldAdd_WhenValid()
    {
        var cls = new ClassModel { Id = Guid.NewGuid(), Methods = new(), IsAbstract = true, IsSealed = false };
        var method = new MethodModel { Name = "M" };

        _classRepo.Setup(r => r.GetById(cls.Id)).Returns(cls);

        _service.AddMethodToClass(cls.Id, method);

        Assert.AreEqual(1, cls.Methods.Count);
        _classRepo.Verify(r => r.Update(cls), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddMethodToClass_ShouldThrow_WhenClassNotFound()
    {
        _classRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ClassModel?)null);
        _service.AddMethodToClass(Guid.NewGuid(), new MethodModel());
    }

    [TestMethod]
    public void GetByName_ShouldReturnMatch()
    {
        var id = Guid.NewGuid();
        var list = new List<MethodModel> { new MethodModel { ClassId = id, Name = "X" } };
        _methodRepo.Setup(r => r.GetAll()).Returns(list);
        var result = _service.GetByName(id, "X");
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void SimulateMethodExecution_ShouldReturnFormattedLines_WhenValid()
    {
        var methodId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        var method = new MethodModel { Id = methodId, Name = "Main", ClassId = classId };
        var classModel = new ClassModel { Id = classId, Name = "Calculator" };

        var call = new MethodCallModel
        {
            Id = Guid.NewGuid(),
            MethodName = "SubMethod",
            ReferenceType = ReferenceTypeInvocation.This
        };

        _methodRepo.Setup(r => r.GetById(methodId)).Returns(method);
        _classRepo.Setup(r => r.GetById(classId)).Returns(classModel);
        _methodRepo.Setup(r => r.GetMethodCalls(methodId)).Returns(new List<MethodCallModel> { call });
        _methodRepo.Setup(r => r.GetMethodCalls(call.Id)).Returns(new List<MethodCallModel>());

        var req = new SimulationRequest
        {
            MethodId = methodId,
            ConcreteTypeId = Guid.NewGuid()
        };

        var result = _service.SimulateMethodExecution(req);

        Assert.AreEqual(2, result.Lines.Count);
        Assert.AreEqual("Calculator.Main()", result.Lines[0]);
        Assert.AreEqual("  this.SubMethod()", result.Lines[1]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SimulateMethodExecution_ShouldThrow_WhenNotFound()
    {
        _methodRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((MethodModel?)null);
        _service.SimulateMethodExecution(new SimulationRequest { MethodId = Guid.NewGuid() });
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenAbstractMethodInConcreteClass()
    {
        var classId = Guid.NewGuid();
        var cls = new ClassModel { Id = classId, IsAbstract = false, IsSealed = false, Methods = new() };
        var method = new MethodModel { Name = "M", IsAbstract = true };
        _classRepo.Setup(r => r.GetById(classId)).Returns(cls);

        Assert.ThrowsException<InvalidOperationException>(() => _service.AddMethodToClass(classId, method));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SimulateMethodExecution_ShouldThrow_WhenClassNotFound()
    {
        var methodId = Guid.NewGuid();
        var method = new MethodModel { Id = methodId, Name = "Main", ClassId = Guid.NewGuid() };

        _methodRepo.Setup(r => r.GetById(methodId)).Returns(method);
        _classRepo.Setup(r => r.GetById(method.ClassId)).Returns((ClassModel?)null);

        var req = new SimulationRequest { MethodId = methodId };
        _service.SimulateMethodExecution(req);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenDuplicateMethod()
    {
        var classId = Guid.NewGuid();
        var cls = new ClassModel { Id = classId, Methods = new() { new MethodModel { Name = "M" } }, IsAbstract = true };
        var method = new MethodModel { Name = "M" };
        _classRepo.Setup(r => r.GetById(classId)).Returns(cls);

        Assert.ThrowsException<InvalidOperationException>(() => _service.AddMethodToClass(classId, method));
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenClassIsSealed()
    {
        var classId = Guid.NewGuid();
        var cls = new ClassModel { Id = classId, IsSealed = true, IsAbstract = true, Methods = new() };
        var method = new MethodModel { Name = "M" };
        _classRepo.Setup(r => r.GetById(classId)).Returns(cls);

        Assert.ThrowsException<InvalidOperationException>(() => _service.AddMethodToClass(classId, method));
    }

    [TestMethod]
    public void AppendCall_ShouldHandleAllReferenceTypes()
    {
        var allTypes = Enum.GetValues(typeof(ReferenceTypeInvocation)).Cast<ReferenceTypeInvocation>();
        foreach (var type in allTypes)
        {
            var call = new MethodCallModel
            {
                Id = Guid.NewGuid(),
                MethodName = "Nested",
                ReferenceType = type,
                ReferenceName = type switch
                {
                    ReferenceTypeInvocation.Attribute => "attr",
                    ReferenceTypeInvocation.Parameter => "param",
                    ReferenceTypeInvocation.LocalVar => "tmp",
                    _ => null
                }
            };

            _methodRepo.Setup(r => r.GetMethodCalls(call.Id)).Returns(new List<MethodCallModel>());

            var lines = new List<string>();
            var method = typeof(MethodModelService).GetMethod("AppendCall", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            method.Invoke(_service, new object[] { lines, call, 1 });

            Assert.AreEqual(1, lines.Count);
            Assert.IsTrue(lines[0].Contains(call.MethodName));
        }
    }

    [TestMethod]
    public void GetMethodToExecute_ShouldReturnOverride_WhenItExists()
    {
        var baseClassId = Guid.NewGuid();
        var derivedClassId = Guid.NewGuid();

        var virtualMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = baseClassId,
            Name = "Run",
            IsVirtual = true
        };

        var overrideMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = derivedClassId,
            Name = "Run",
            IsOverride = true
        };

        var baseClass = new ClassModel { Id = baseClassId, Methods = new List<MethodModel> { virtualMethod } };
        var derivedClass = new ClassModel { Id = derivedClassId, Methods = new List<MethodModel> { overrideMethod }, BaseClassId = baseClassId };

        _classRepo.Setup(r => r.GetById(baseClassId)).Returns(baseClass);
        _classRepo.Setup(r => r.GetById(derivedClassId)).Returns(derivedClass);

        var result = _service.GetMethodToExecute("Run", baseClassId, derivedClassId);

        Assert.IsNotNull(result);
        Assert.AreEqual(overrideMethod.Id, result.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenOverrideWithoutVirtualBase()
    {
        var baseClassId = Guid.NewGuid();
        var derivedClassId = Guid.NewGuid();

        var baseMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = baseClassId,
            Name = "Run",
            IsVirtual = false
        };

        var overrideMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = derivedClassId,
            Name = "Run",
            IsOverride = true
        };

        var baseClass = new ClassModel { Id = baseClassId, Methods = new List<MethodModel> { baseMethod } };
        var derivedClass = new ClassModel { Id = derivedClassId, Methods = new List<MethodModel>(), BaseClassId = baseClassId };

        _classRepo.Setup(r => r.GetById(baseClassId)).Returns(baseClass);
        _classRepo.Setup(r => r.GetById(derivedClassId)).Returns(derivedClass);

        _service.Add(overrideMethod);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenOverrideWithoutBaseMethod()
    {
        var baseClassId = Guid.NewGuid();
        var derivedClassId = Guid.NewGuid();

        var overrideMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = derivedClassId,
            Name = "Run",
            IsOverride = true
        };

        var baseClass = new ClassModel { Id = baseClassId, Methods = new List<MethodModel>() };
        var derivedClass = new ClassModel { Id = derivedClassId, Methods = new List<MethodModel>(), BaseClassId = baseClassId };

        _classRepo.Setup(r => r.GetById(baseClassId)).Returns(baseClass);
        _classRepo.Setup(r => r.GetById(derivedClassId)).Returns(derivedClass);

        _service.Add(overrideMethod);
    }

    [TestMethod]
    public void Add_ShouldSucceed_WhenOverrideWithVirtualBase()
    {
        var baseClassId = Guid.NewGuid();
        var derivedClassId = Guid.NewGuid();

        var baseMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = baseClassId,
            Name = "Run",
            IsVirtual = true
        };

        var overrideMethod = new MethodModel
        {
            Id = Guid.NewGuid(),
            ClassId = derivedClassId,
            Name = "Run",
            IsOverride = true
        };

        var baseClass = new ClassModel { Id = baseClassId, Methods = new List<MethodModel> { baseMethod } };
        var derivedClass = new ClassModel { Id = derivedClassId, Methods = new List<MethodModel>(), BaseClassId = baseClassId };

        _classRepo.Setup(r => r.GetById(baseClassId)).Returns(baseClass);
        _classRepo.Setup(r => r.GetById(derivedClassId)).Returns(derivedClass);

        _service.Add(overrideMethod);

        _methodRepo.Verify(r => r.Add(overrideMethod), Times.Once);
    }
}
