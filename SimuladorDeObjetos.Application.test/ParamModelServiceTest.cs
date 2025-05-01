using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class ParamModelServiceTest
{
    private Mock<IRepositoryParamModel>? _mockRepository;
    private ParamModelService? _service;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepository = new Mock<IRepositoryParamModel>();
        _service = new ParamModelService(_mockRepository.Object);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllParamModels()
    {
        var models = new List<ParamModel>
        {
            new ParamModel { Id = Guid.NewGuid(), Name = "Param1" },
            new ParamModel { Id = Guid.NewGuid(), Name = "Param2" }
        };

        _mockRepository.Setup(r => r.GetAll()).Returns(models);

        var result = _service.GetAll().ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Param1", result[0].Name);
    }

    [TestMethod]
    public void Add_ShouldCallRepositoryAddAndSave()
    {
        var model = new ParamModel { Id = Guid.NewGuid(), Name = "NewParam" };

        _service.Add(model);

        _mockRepository.Verify(r => r.Add(model), Times.Once);
        _mockRepository.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdateAndSave()
    {
        var model = new ParamModel { Id = Guid.NewGuid(), Name = "UpdatedParam" };

        _service.Update(model);

        _mockRepository.Verify(r => r.Update(model), Times.Once);
        _mockRepository.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDeleteAndSave()
    {
        var model = new ParamModel { Id = Guid.NewGuid(), Name = "DeletedParam" };

        _service.Delete(model);

        _mockRepository.Verify(r => r.Delete(model), Times.Once);
        _mockRepository.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void SaveChanges_ShouldCallRepositorySaveChanges()
    {
        _service.SaveChanges();

        _mockRepository.Verify(r => r.SaveChanges(), Times.Once);
    }
}
