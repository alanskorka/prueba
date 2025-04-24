using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class ClassModelRepositoryTest
{
    private Mock<DbSet<ClassModel>>? _mockSet;
    private Mock<DbContext>? _mockContext;
    private ClassModelRepository? _repository;

    private void SetupMocks(IQueryable<ClassModel> data)
    {
        _mockSet = new Mock<DbSet<ClassModel>>();
        _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        _mockContext = new Mock<DbContext>();
        _mockContext.Setup(c => c.Set<ClassModel>()).Returns(_mockSet.Object);

        _repository = new ClassModelRepository(_mockContext.Object);
    }

    [TestMethod]
    public void Add_ShouldStoreClassModel()
    {
        var data = new List<ClassModel>
        {
            new ClassModel { Name = "Test1" },
            new ClassModel { Name = "Test2" }
        }.AsQueryable();
        SetupMocks(data);
        var result = _repository.GetAll().ToList();
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Test1", result[0].Name);
    }
}
