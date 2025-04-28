using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class LocalVarModelRepositoryTest
{
    private Mock<DbSet<LocalVarModel>>? _mockSet;
    private Mock<DbContext>? _mockContext;
    private LocalVarModelRepository? _repository;

    private void SetupMocks(IQueryable<LocalVarModel> data)
    {
        _mockSet = new Mock<DbSet<LocalVarModel>>();
        _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        _mockContext = new Mock<DbContext>();
        _mockContext.Setup(c => c.Set<LocalVarModel>()).Returns(_mockSet.Object);

        _repository = new LocalVarModelRepository(_mockContext.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllLocalVarModels()
    {
        var data = new List<LocalVarModel> { new LocalVarModel { Name = "Var1" }, new LocalVarModel { Name = "Var2" } }
            .AsQueryable();

        SetupMocks(data);

        var result = _repository.GetAll().ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Var1", result[0].Name);
    }

    [TestMethod]
    public void Add_ShouldAddLocalVarModel()
    {
        var data = new List<LocalVarModel>().AsQueryable();
        SetupMocks(data);

        var localVar = new LocalVarModel { Name = "NewVar" };
        _repository.Add(localVar);

        _mockSet!.Verify(m => m.Add(localVar), Times.Once);
    }
}
