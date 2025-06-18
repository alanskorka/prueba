using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class AttributeModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Guid ClassId { get; set; }
    public AccessibilityModifier Accessibility { get; set; }
    public bool IsStatic { get; set; } = false;
    public Guid? ConcreteTypeId { get; set; }

    [JsonIgnore]
    public ClassModel? Class { get; set; }

    [JsonIgnore]
    public ClassModel? ConcreteType { get; set; }
}
