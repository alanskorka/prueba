using Domain.Entities;

namespace SimuladorDeObjetos.Application.DTOs;

public class ClassDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsAbstract { get; set; }
    public Guid? BaseClassId { get; set; }
    public bool IsSealed { get; set; }

    public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();
    public List<MethodModel> Methods { get; set; } = new List<MethodModel>();
    public ClassModel? BaseClass { get; set; }
}
