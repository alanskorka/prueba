using SimuladorDeObjetos.Application.DTOs.Api;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class SimulationRequestDtoTest
{
    [TestMethod]
    public void SimulationRequest_AllProperties_GettersAndSettersWork()
    {
        var expectedReferenceTypeId = Guid.NewGuid();
        var expectedConcreteTypeId  = Guid.NewGuid();
        var expectedMethodId        = Guid.NewGuid();
        var dto = new SimulationRequest
        {
            ReferenceTypeId = expectedReferenceTypeId,
            ConcreteTypeId  = expectedConcreteTypeId,
            MethodId        = expectedMethodId
        };
        Assert.AreEqual(expectedReferenceTypeId, dto.ReferenceTypeId,  "ReferenceTypeId getter/setter failed");
        Assert.AreEqual(expectedConcreteTypeId,  dto.ConcreteTypeId,  "ConcreteTypeId getter/setter failed");
        Assert.AreEqual(expectedMethodId,        dto.MethodId,        "MethodId getter/setter failed");
    }
}
