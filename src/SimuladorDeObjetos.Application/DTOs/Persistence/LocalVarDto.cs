using Domain.Entities;

namespace SimuladorDeObjetos.Application.DTOs;

public class LocalVarDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Guid MethodId { get; set; }
    public MethodModel Method { get; set; } = null!;
}
