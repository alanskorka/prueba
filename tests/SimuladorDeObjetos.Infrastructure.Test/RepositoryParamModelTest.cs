using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class RepositoryParamModelTest
{
    private ParamModel? _param;
    private IQueryable<ParamModel>? _data;
    private Mock<DbSet<ParamModel>>? _mockSet;
    private Mock<DbContext>? _mockContext;
    private RepositoryParamModel? _repo;

    [TestInitialize]
    public void Setup()
    {
        _param = new ParamModel
        {
            Id = Guid.NewGuid(),
            Name = "p1",
            Type = "int",
            MethodId = Guid.NewGuid()
        };

        _data = new List<ParamModel> { _param }.AsQueryable();

        _mockSet = new Mock<DbSet<ParamModel>>();
        _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.Provider).Returns(_data.Provider);
        _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.Expression).Returns(_data.Expression);
        _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
        _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

        _mockContext = new Mock<DbContext>();
        _mockContext.Setup(c => c.Set<ParamModel>()).Returns(_mockSet.Object);

        _repo = new RepositoryParamModel(_mockContext.Object);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _repo.Add(_param ?? throw new InvalidOperationException());

        _mockSet.Verify(m => m.Add(_param), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenParamIsNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllParams()
    {
        var result = _repo.GetAll();
        CollectionAssert.AreEqual((_data ?? throw new InvalidOperationException()).ToList(), result);
    }
}
