using Domain.Enums;

namespace SimuladorDeObjetos.Application.DTOs;

public class AttributeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid ClassId { get; set; }
    public AccessibilityModifier Accessibility { get; set; }
}
