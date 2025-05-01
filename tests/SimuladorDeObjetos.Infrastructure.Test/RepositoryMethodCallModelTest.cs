using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class RepositoryMethodCallModelTest
{
    private MethodCallModel? _call;
    private IQueryable<MethodCallModel>? _data;
    private Mock<DbSet<MethodCallModel>>? _mockSet;
    private Mock<DbContext>? _mockContext;
    private RepositoryMethodCallModel? _repo;

    [TestInitialize]
    public void Setup()
    {
        _call = new MethodCallModel
        {
            MethodName = "TestCall",
            ReferenceType = ReferenceTypeInvocation.This
        };

        _data = new List<MethodCallModel> { _call }.AsQueryable();

        _mockSet = new Mock<DbSet<MethodCallModel>>();
        _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.Provider).Returns(_data.Provider);
        _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.Expression).Returns(_data.Expression);
        _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
        _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

        _mockContext = new Mock<DbContext>();
        _mockContext.Setup(c => c.Set<MethodCallModel>()).Returns(_mockSet.Object);

        _repo = new RepositoryMethodCallModel(_mockContext.Object);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _repo.Add(_call ?? throw new InvalidOperationException());

        _mockSet.Verify(m => m.Add(_call), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenCallIsNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllCalls()
    {
        var result = _repo.GetAll();

        CollectionAssert.AreEqual((_data ?? throw new InvalidOperationException()).ToList(), result);
    }

    [TestMethod]
    public void Update_ShouldCallUpdateAndSaveChanges()
    {
        var updated = new MethodCallModel
        {
            MethodName = "UpdatedCall",
            ReferenceType = ReferenceTypeInvocation.Base
        };
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _repo.Update(updated);

        _mockSet.Verify(m => m.Update(updated), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenCallIsNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldCallRemoveAndSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _repo.Delete(_call ?? throw new InvalidOperationException());

        _mockSet.Verify(m => m.Remove(_call), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenCallIsNull()
    {
        _repo.Delete(null!);
    }

    [TestMethod]
    public void SaveChanges_ShouldCallContextSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);
        _repo.SaveChanges();
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }
}
