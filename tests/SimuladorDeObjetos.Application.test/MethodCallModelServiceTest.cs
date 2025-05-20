using Domain.Entities;
using Domain.Enums;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MethodCallModelServiceTest
{
    private Mock<IMethodCallModelRepository>? _mockRepo;
    private MethodCallModelService? _service;
    private MethodCallModel? _call;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IMethodCallModelRepository>();
        _service = new MethodCallModelService(_mockRepo.Object);

        _call = new MethodCallModel
        {
            Id = Guid.NewGuid(),
            MethodName = "M1",
            ReferenceType = ReferenceTypeInvocation.This,
            ParentMethodId = Guid.NewGuid(),
            ReferenceName = "this"
        };
    }

    [TestMethod]
    public void Create_ShouldCallRepositoryAdd()
    {
        _service!.Create(_call!);
        _mockRepo!.Verify(r => r.Add(_call!), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Create_ShouldThrow_WhenCallIsNull()
    {
        _service!.Create(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Create_ShouldThrow_WhenCallAlreadyExists()
    {
        _mockRepo!.Setup(r => r.GetById(_call!.Id)).Returns(_call);
        _service!.Create(_call!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnListFromRepository()
    {
        var list = new List<MethodCallModel> { _call! };
        _mockRepo!.Setup(r => r.GetAll()).Returns(list);

        var result = _service!.GetAll();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("M1", result[0].MethodName);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdate()
    {
        _mockRepo!.Setup(r => r.GetById(_call!.Id)).Returns(_call);

        _service!.Update(_call!);

        _mockRepo.Verify(r => r.Update(_call!), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenCallIsNull()
    {
        _service!.Update(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenCallDoesNotExist()
    {
        _mockRepo!.Setup(r => r.GetById(_call!.Id)).Returns((MethodCallModel?)null);
        _service!.Update(_call!);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete()
    {
        _mockRepo!.Setup(r => r.GetById(_call!.Id)).Returns(_call);

        _service!.Delete(_call!);

        _mockRepo.Verify(r => r.Delete(_call!), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenCallIsNull()
    {
        _service!.Delete(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Delete_ShouldThrow_WhenCallDoesNotExist()
    {
        _mockRepo!.Setup(r => r.GetById(_call!.Id)).Returns((MethodCallModel?)null);
        _service!.Delete(_call!);
    }
}
