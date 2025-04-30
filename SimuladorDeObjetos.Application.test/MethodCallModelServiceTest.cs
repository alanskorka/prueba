using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MethodCallModelServiceTest
{
    private Mock<IRepositoryMethodCallModel>? _mockRepo;
    private MethodCallModelService? _service;
    private MethodCallModel? _call;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IRepositoryMethodCallModel>();
        _service = new MethodCallModelService(_mockRepo.Object);
        _call = new MethodCallModel
        {
            MethodName = "M1",
            ReferenceType = ReferenceTypeInvocation.This
        };
    }

    [TestMethod]
    public void Create_ShouldCallRepositoryAdd()
    {
        _service.Create(_call ?? throw new InvalidOperationException());
        _mockRepo.Verify(r => r.Add(_call), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Create_ShouldThrow_WhenCallIsNull()
    {
        _service.Create(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnListFromRepository()
    {
        var list = new List<MethodCallModel> { _call ?? throw new InvalidOperationException() };
        _mockRepo.Setup(r => r.GetAll()).Returns(list);

        var result = _service.GetAll();

        CollectionAssert.AreEqual(list, result);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdate()
    {
        _service.Update(_call ?? throw new InvalidOperationException());
        _mockRepo.Verify(r => r.Update(_call), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenCallIsNull()
    {
        _service.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete()
    {
        _service.Delete(_call ?? throw new InvalidOperationException());
        _mockRepo.Verify(r => r.Delete(_call), Times.Once);
    }
}
