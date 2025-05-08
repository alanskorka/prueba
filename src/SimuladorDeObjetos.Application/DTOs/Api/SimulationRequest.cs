namespace SimuladorDeObjetos.Application.DTOs.Api;

public class SimulationRequest
{
    public Guid ReferenceTypeId { get; set; }
    public Guid ConcreteTypeId { get; set; }
    public Guid MethodId { get; set; }
}
