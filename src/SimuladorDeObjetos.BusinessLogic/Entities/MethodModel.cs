using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class MethodModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public Guid ClassId { get; set; }
    public bool IsAbstract { get; set; }
    public bool IsSealed { get; set; }
    public AccessibilityModifier Accessibility { get; set; }
    public bool IsVirtual { get; set; } = false;
    public bool IsOverride { get; set; } = false;
    public List<ParamModel> Params { get; set; } = new();
    public List<LocalVarModel> Vars { get; set; } = new();

    [JsonIgnore]
    public List<MethodCallModel>? MethodsCalled { get; set; } = new();

    [JsonIgnore]
    public ClassModel? Class { get; set; } = null!;
}
