using Domain.Entities;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace Tests;

[TestClass]
public class NamespaceServiceTests
{
    private Mock<INamespaceRepository> _repoMock = null!;
    private NamespaceService _service = null!;

    [TestInitialize]
    public void SetUp()
    {
        _repoMock = new Mock<INamespaceRepository>();
        _service = new NamespaceService(_repoMock.Object);
    }

    [TestMethod]
    public void AddNamespace_ShouldCallRepositoryAdd()
    {
        var model = new NamespaceModel { Id = Guid.NewGuid(), Name = "MyNamespace" };

        _service.AddNamespace(model);

        _repoMock.Verify(r => r.Add(model), Times.Once);
    }

    [TestMethod]
    public void DeleteNamespace_ShouldCallRepositoryRemove_WhenNamespaceExists()
    {
        var id = Guid.NewGuid();
        var model = new NamespaceModel { Id = id, Name = "ToDelete" };
        _repoMock.Setup(r => r.GetById(id)).Returns(model);

        _service.DeleteNamespace(id);

        _repoMock.Verify(r => r.Remove(model), Times.Once);
    }

    [TestMethod]
    public void DeleteNamespace_ShouldNotCallRemove_WhenNamespaceDoesNotExist()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.GetById(id)).Returns((NamespaceModel?)null);

        _service.DeleteNamespace(id);

        _repoMock.Verify(r => r.Remove(It.IsAny<NamespaceModel>()), Times.Never);
    }

    [TestMethod]
    public void GetById_ShouldReturnCorrectNamespace()
    {
        var id = Guid.NewGuid();
        var model = new NamespaceModel { Id = id, Name = "GetMe" };
        _repoMock.Setup(r => r.GetById(id)).Returns(model);

        var result = _service.GetById(id);

        Assert.AreEqual("GetMe", result?.Name);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllNamespaces()
    {
        var list = new List<NamespaceModel>
        {
            new() { Name = "One" },
            new() { Name = "Two" }
        };
        _repoMock.Setup(r => r.GetAll()).Returns(list);

        var result = _service.GetAll();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("One", result[0].Name);
        Assert.AreEqual("Two", result[1].Name);
    }
}
