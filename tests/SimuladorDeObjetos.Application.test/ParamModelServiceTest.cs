using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class ParamModelServiceTest
{
    private Mock<IParamModelRepository>? _mockRepo;
    private Mock<IMethodModelRepository>? _mockMethodRepo;
    private ParamModelService? _service;
    private ParamModel? _param;
    private MethodModel? _method;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepo = new Mock<IParamModelRepository>();
        _mockMethodRepo = new Mock<IMethodModelRepository>();
        _service = new ParamModelService(_mockRepo.Object, _mockMethodRepo.Object);

        _method = new MethodModel
        {
            Id = Guid.NewGuid(),
            Name = "MethodA",
            Params = new List<ParamModel>()
        };

        _param = new ParamModel
        {
            Id = Guid.NewGuid(),
            Name = "p1",
            Type = "int",
            MethodId = _method.Id
        };
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllParamModels()
    {
        var models = new List<ParamModel> { _param! };
        _mockRepo!.Setup(r => r.GetAll()).Returns(models);

        var result = _service!.GetAll().ToList();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("p1", result[0].Name);
    }

    [TestMethod]
    public void Add_ShouldCallRepositoryAddAndSave_WhenValid()
    {
        _mockMethodRepo!.Setup(r => r.GetById(_method!.Id)).Returns(_method);

        _service!.Add(_param!);

        _mockRepo!.Verify(r => r.Add(_param!), Times.Once);
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenNull()
    {
        _service!.Add(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Add_ShouldThrow_WhenMethodNotFound()
    {
        _mockMethodRepo!.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((MethodModel?)null);
        _service!.Add(_param!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenDuplicateParamName()
    {
        _method!.Params.Add(new ParamModel { Name = _param!.Name });
        _mockMethodRepo!.Setup(r => r.GetById(_method.Id)).Returns(_method);
        _service!.Add(_param!);
    }

    [TestMethod]
    public void Update_ShouldCallRepositoryUpdateAndSave()
    {
        var otherParam = new ParamModel
        {
            Id = Guid.NewGuid(),
            Name = "otherParam"
        };

        var method = new MethodModel
        {
            Id = _param!.MethodId,
            Params = new List<ParamModel> { _param, otherParam }
        };

        _mockRepo!.Setup(r => r.GetById(_param.Id)).Returns(_param);
        _mockMethodRepo!.Setup(r => r.GetById(_param.MethodId)).Returns(method);

        _service!.Update(_param);

        _mockRepo.Verify(r => r.Update(_param), Times.Once);
        _mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _service!.Update(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenParamNotFound()
    {
        _mockRepo!.Setup(r => r.GetById(_param!.Id)).Returns((ParamModel?)null);
        _service!.Update(_param!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenDuplicateNameExists()
    {
        var otherParam = new ParamModel { Id = Guid.NewGuid(), Name = _param!.Name };

        _mockRepo!.Setup(r => r.GetById(_param!.Id)).Returns(_param);
        _mockMethodRepo!.Setup(r => r.GetById(_param.MethodId))
            .Returns(new MethodModel
            {
                Id = _param.MethodId,
                Params = new List<ParamModel> { _param!, otherParam }
            });

        _service!.Update(new ParamModel
        {
            Id = otherParam.Id,
            Name = _param.Name,
            MethodId = _param.MethodId
        });
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenMethodNotFound()
    {
        _mockRepo!.Setup(r => r.GetById(_param!.Id)).Returns(_param);
        _mockMethodRepo!.Setup(r => r.GetById(_param.MethodId)).Returns((MethodModel?)null);

        _service!.Update(_param!);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDeleteAndSave()
    {
        _service!.Delete(_param!);

        _mockRepo!.Verify(r => r.Delete(_param!), Times.Once);
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenNull()
    {
        _service!.Delete(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Delete_ShouldThrow_WhenParamNotFound()
    {
        _mockRepo!.Setup(r => r.GetById(_param!.Id)).Returns((ParamModel?)null);
        _service!.Delete(_param!);
    }

    [TestMethod]
    public void SaveChanges_ShouldCallRepositorySaveChanges()
    {
        _service!.SaveChanges();
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }
}
