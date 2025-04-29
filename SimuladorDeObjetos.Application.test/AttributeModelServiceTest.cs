using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class AttributeModelServiceTest
{
    private Mock<IRepositoryAtributteModel>? _mockRepo;
    private AttributeModelService? _service;
    private AttributeModel? _attribute;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IRepositoryAtributteModel>();
        _service = new AttributeModelService(_mockRepo.Object);
        _attribute = new AttributeModel
        {
            Id = Guid.NewGuid(),
            Name = "Attr",
            Type = "string",
            ClassId = Guid.NewGuid(),
            Accessibility = Domain.Enums.AccessibilityModifier.Public
        };
    }
}
