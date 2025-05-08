using Domain.Enums;

namespace Domain.Entities;

public class AttributeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid ClassId { get; set; }
    public AccessibilityModifier Accessibility { get; set; }
    public ClassModel Class { get; set; } = null!;
}
