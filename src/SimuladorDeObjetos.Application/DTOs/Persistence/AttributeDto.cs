using Domain.Entities;
using Domain.Enums;

namespace SimuladorDeObjetos.Application.DTOs.Persistence;

public class AttributeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid ClassId { get; set; }
    public bool IsStatic { get; set; }
    public AccessibilityModifier Accessibility { get; set; }
    public ClassModel Class { get; set; } = null!;
    public Guid? ConcreteTypeId { get; set; }
    public ClassModel? ConcreteType { get; set; }
}
