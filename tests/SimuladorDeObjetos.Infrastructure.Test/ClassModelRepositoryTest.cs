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

    [TestMethod]
    public void GetAll_ShouldReturnAllClassModels()
    {
        var data = new List<ClassModel>
        {
            new ClassModel { Name = "ClassA" },
            new ClassModel { Name = "ClassB" },
            new ClassModel { Name = "ClassC" }
        }.AsQueryable();

        SetupMocks(data);

        var result = _repository.GetAll().ToList();

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("ClassA", result[0].Name);
        Assert.AreEqual("ClassB", result[1].Name);
        Assert.AreEqual("ClassC", result[2].Name);
    }

    [TestMethod]
    public void Delete_ShouldRemoveClassModel()
    {
        var classModelToDelete = new ClassModel { Id = Guid.NewGuid(), Name = "ToDelete" };
        var data = new List<ClassModel>
        {
            classModelToDelete,
            new ClassModel { Id = Guid.NewGuid(), Name = "KeepMe" }
        }.AsQueryable();

        SetupMocks(data);

        _repository.Delete(classModelToDelete);

        _mockSet.Verify(m => m.Remove(classModelToDelete), Times.Once);
    }

    [TestMethod]
    public void Update_ShouldModifyClassModelInDatabase()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new SimuladorDbContext(options);
        var repository = new ClassModelRepository(context);

        var model = new ClassModel { Id = Guid.NewGuid(), Name = "Original" };
        context.Add(model);
        context.SaveChanges();

        model.Name = "Updated";
        repository.Update(model);
        repository.SaveChanges();

        var updated = context.Classes.First(x => x.Id == model.Id);
        Assert.AreEqual("Updated", updated.Name);
    }

    [TestMethod]
    public void SaveChanges_ShouldPersistChanges()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: "SaveChangesTestDb")
            .Options;

        using var context = new SimuladorDbContext(options);
        var repository = new ClassModelRepository(context);

        var model = new ClassModel { Id = Guid.NewGuid(), Name = "ToPersist" };
        repository.Add(model);
        repository.SaveChanges();

        var persisted = context.Classes.FirstOrDefault(c => c.Id == model.Id);
        Assert.IsNotNull(persisted);
        Assert.AreEqual("ToPersist", persisted.Name);
    }

    [TestMethod]
    public void GetById_ShouldReturnEntity_WhenExists()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdExistsDb")
            .Options;

        using var context = new SimuladorDbContext(options);
        var repo = new ClassModelRepository(context);

        var model = new ClassModel { Id = Guid.NewGuid(), Name = "FindMe" };
        context.Add(model);
        context.SaveChanges();

        var result = repo.GetById(model.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual("FindMe", result!.Name);
    }

    [TestMethod]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdNotExistsDb")
            .Options;

        using var context = new SimuladorDbContext(options);
        var repo = new ClassModelRepository(context);

        var result = repo.GetById(Guid.NewGuid());

        Assert.IsNull(result);
    }
}
