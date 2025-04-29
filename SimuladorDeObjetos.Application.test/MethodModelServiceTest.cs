using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MethodModelServiceTest
{
    private Mock<IMethodModelRepository>? _methodRepo;
    private Mock<IClassModelRepository>? _classRepo;
    private MethodModelService? _service;

    [TestInitialize]
    public void Init()
    {
        _methodRepo = new Mock<IMethodModelRepository>();
        _classRepo = new Mock<IClassModelRepository>();
        _service = new MethodModelService(_methodRepo.Object, _classRepo.Object);
    }

    [TestMethod]
    public void AddMethodToClass_ShouldThrow_WhenClassNotFound()
    {
        _classRepo!.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ClassModel?)null);

        var ex = Assert.ThrowsException<Exception>(() =>
            _service!.AddMethodToClass(Guid.NewGuid(), new MethodModel()));

        Assert.AreEqual("Class not found", ex.Message);
    }
}
