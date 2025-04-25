using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class RepositoryAtributteModelTest
{
    private AttributeModel? _attribute;
    private IQueryable<AttributeModel?>? _data;
    private Mock<DbSet<AttributeModel>>? _mockSet;
    private Mock<DbContext>? _mockContext;
    private RepositoryAtributteModel? _repo;

    [TestInitialize]
    public void Setup()
    {
        _attribute = new AttributeModel
        {
            Id = Guid.NewGuid(),
            Name = "TestAttribute",
            Type = "string",
            ClassId = Guid.NewGuid(),
            Accessibility = AccessibilityModifier.Public
        };

        _data = new List<AttributeModel?> { _attribute }.AsQueryable();

        _mockSet = new Mock<DbSet<AttributeModel>>();
        _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.Provider).Returns(_data.Provider);
        _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.Expression).Returns(_data.Expression);
        _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
        _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

        _mockContext = new Mock<DbContext>();
        _mockContext.Setup(c => c.Set<AttributeModel>()).Returns(_mockSet.Object);

        _repo = new RepositoryAtributteModel(_mockContext.Object);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);
        _repo.Add(_attribute ?? throw new InvalidOperationException());
        _mockSet.Verify(m => m.Add(_attribute), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenAttributeIsNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllAttributes()
    {
        var result = _repo.GetAll();
        CollectionAssert.AreEqual((_data ?? throw new InvalidOperationException()).ToList(), result);
    }
}
