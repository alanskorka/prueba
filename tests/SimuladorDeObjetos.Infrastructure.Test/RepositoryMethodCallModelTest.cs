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
}
