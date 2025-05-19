using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class LocalVarModelServiceTest
{
    private Mock<ILocalVarModelRepository>? _mockRepo;
    private Mock<IMethodModelRepository>? _mockMethodRepo;
    private LocalVarModelService? _service;
    private MethodModel? _method;
    private LocalVarModel? _var;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepo = new Mock<ILocalVarModelRepository>();
        _mockMethodRepo = new Mock<IMethodModelRepository>();
        _service = new LocalVarModelService(_mockRepo.Object, _mockMethodRepo.Object);

        _method = new MethodModel
        {
            Id = Guid.NewGuid(),
            Name = "Method1",
            Vars = new List<LocalVarModel>()
        };

        _var = new LocalVarModel
        {
            Id = Guid.NewGuid(),
            Name = "var1",
            Type = "int",
            MethodId = _method.Id
        };
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllLocalVarModels()
    {
        var expected = new List<LocalVarModel>
        {
            new LocalVarModel { Name = "Var1" },
            new LocalVarModel { Name = "Var2" }
        };

        _mockRepo!.Setup(r => r.GetAll()).Returns(expected);

        var result = _service!.GetAll().ToList();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Var1", result[0].Name);
    }

    [TestMethod]
    public void Add_ShouldCallAddAndSaveChanges()
    {
        _mockMethodRepo!.Setup(r => r.GetById(_method!.Id)).Returns(_method);
        _service!.Add(_var!);

        _mockRepo!.Verify(r => r.Add(_var!), Times.Once);
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenVarIsNull()
    {
        _service!.Add(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Add_ShouldThrow_WhenMethodNotFound()
    {
        _mockMethodRepo!.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((MethodModel?)null);
        _service!.Add(_var!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Add_ShouldThrow_WhenDuplicateVarName()
    {
        _method!.Vars.Add(new LocalVarModel { Name = _var!.Name });
        _mockMethodRepo!.Setup(r => r.GetById(_method.Id)).Returns(_method);
        _service!.Add(_var!);
    }

    [TestMethod]
    public void Update_ShouldCallUpdateAndSaveChanges()
    {
        _service!.Update(_var!);

        _mockRepo!.Verify(r => r.Update(_var!), Times.Once);
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _service!.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldCallDeleteAndSaveChanges()
    {
        _service!.Delete(_var!);

        _mockRepo!.Verify(r => r.Delete(_var!), Times.Once);
        _mockRepo!.Verify(r => r.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Update_ShouldThrow_WhenDuplicateVarNameExists()
    {
        var otherVar = new LocalVarModel { Id = Guid.NewGuid(), Name = _var!.Name };

        _mockMethodRepo!.Setup(r => r.GetById(_var!.MethodId)).Returns(new MethodModel
        {
            Id = _var.MethodId,
            Vars = new List<LocalVarModel> { _var!, otherVar }
        });

        _mockRepo!.Setup(r => r.GetById(_var.Id)).Returns(_var);

        _service!.Update(new LocalVarModel
        {
            Id = otherVar.Id,
            Name = _var.Name,
            MethodId = _var.MethodId
        });
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenNull()
    {
        _service!.Delete(null!);
    }
}
