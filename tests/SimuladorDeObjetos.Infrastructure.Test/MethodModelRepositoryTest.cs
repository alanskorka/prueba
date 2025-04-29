using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class MethodModelRepositoryTest
{
    private MethodModel? _method;
    private IQueryable<MethodModel?>? _data;
    private Mock<DbSet<MethodModel>>? _mockSet;
    private Mock<DbContext>? _mockContext;
    private MethodModelRepository? _repo;

    [TestInitialize]
    public void Setup()
    {
        _method = new MethodModel
        {
            Id = Guid.NewGuid(), Name = "TestMethod", ReturnType = "void", ClassId = Guid.NewGuid()
        };

        _data = new List<MethodModel?> { _method }.AsQueryable();
        _mockSet = new Mock<DbSet<MethodModel>>();
        _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.Provider).Returns(_data.Provider);
        _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.Expression).Returns(_data.Expression);
        _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
        _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());
        _mockContext = new Mock<DbContext>();
        _mockContext.Setup(c => c.Set<MethodModel>()).Returns(_mockSet.Object);
        _repo = new MethodModelRepository(_mockContext.Object);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);
        if(_method != null)
        {
            _repo.Add(_method);

            _mockSet.Verify(m => m.Add(_method), Times.Once);
        }

        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllMethods()
    {
        var result = _repo.GetAll();

        if(_data != null)
        {
            CollectionAssert.AreEqual(_data.ToList(), result);
        }
    }

    [TestMethod]
    public void Delete_ShouldCallRemoveAndSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        if(_method != null)
        {
            _repo.Delete(_method);

            _mockSet.Verify(m => m.Remove(_method), Times.Once);
        }

        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenMethodIsNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenMethodIsNull()
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

    [TestMethod]
    public void Update_ShouldCallUpdateAndSaveChanges()
    {
        var updated = new MethodModel
        {
            Id = _method.Id,
            Name = "Updated",
            ReturnType = "int",
            ClassId = _method.ClassId
        };
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);
        _repo.Update(updated);
        _mockSet.Verify(m => m.Update(updated), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenMethodIsNull()
    {
        _repo.Update(null!);
    }
}
