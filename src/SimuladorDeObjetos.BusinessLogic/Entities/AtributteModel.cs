using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class AttributeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid ClassId { get; set; }
    public bool IsStatic { get; set; } = false;
    public AccessibilityModifier Accessibility { get; set; }
    [JsonIgnore]
    public ClassModel? Class { get; set; } = null!;
}
