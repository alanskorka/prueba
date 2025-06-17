using Domain.Entities;

namespace SimuladorDeObjetos.Application.DTOs.Persistence;

public class ClassDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsAbstract { get; set; }
    public Guid? BaseClassId { get; set; }
    public bool IsSealed { get; set; }

    public List<AttributeModel> Attributes { get; set; } = [];
    public List<MethodModel> Methods { get; set; } = [];
    public ClassModel? BaseClass { get; set; }
    public List<InterfaceDto> ImplementedInterfaces { get; set; } = [];
}
