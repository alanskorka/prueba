using Domain.Entities;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MethodCallModelServiceTest
{
    private Mock<IRepositoryMethodCallModel>? _mockRepo;
    private MethodCallModelService? _service;
    private MethodCallModel? _call;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IRepositoryMethodCallModel>();
        _service = new MethodCallModelService(_mockRepo.Object);
        _call = new MethodCallModel
        {
            MethodName = "M1",
            ReferenceType = ReferenceTypeInvocation.This
        };
    }
}
