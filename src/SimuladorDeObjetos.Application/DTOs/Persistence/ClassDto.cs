namespace SimuladorDeObjetos.Application.DTOs;

public class ClassDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsAbstract { get; set; }
    public Guid? BaseClassId { get; set; }
    public bool IsSealed { get; set; }

    public List<AttributeDto> Attributes { get; set; } = new();
    public List<MethodDto> Methods { get; set; } = new();
}
