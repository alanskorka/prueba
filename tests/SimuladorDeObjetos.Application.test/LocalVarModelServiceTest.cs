using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class LocalVarModelServiceTest
{
    private Mock<ILocalVarModelRepository>? _mockRepository;
    private LocalVarModelService? _service;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepository = new Mock<ILocalVarModelRepository>();
        _service = new LocalVarModelService(_mockRepository.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllLocalVarModels()
    {
        var expected = new List<LocalVarModel>
        {
            new LocalVarModel { Name = "Var1" },
            new LocalVarModel { Name = "Var2" }
        };

        _mockRepository!.Setup(r => r.GetAll()).Returns(expected);

        var result = _service!.GetAll().ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Var1", result[0].Name);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        var model = new LocalVarModel { Name = "TestVar" };

        _service!.Add(model);

        _mockRepository!.Verify(r => r.Add(model), Times.Once);
        _mockRepository!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Update_ShouldCallUpdateAndSaveChanges()
    {
        var model = new LocalVarModel { Name = "UpdatedVar" };

        _service!.Update(model);

        _mockRepository!.Verify(r => r.Update(model), Times.Once);
        _mockRepository!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Delete_ShouldCallDeleteAndSaveChanges()
    {
        var model = new LocalVarModel { Name = "ToDelete" };

        _service!.Delete(model);

        _mockRepository!.Verify(r => r.Delete(model), Times.Once);
        _mockRepository!.Verify(r => r.SaveChanges(), Times.Once);
    }
}
