using Domain.Entities;
using Moq;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class ParamModelServiceTest
{
    private Mock<IParamModelRepository> _mockRepository;
    private ParamModelService _service;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepository = new Mock<IParamModelRepository>();
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

