using Domain.Entities;
using Domain.Enums;

namespace SimuladorDeObjetos.Application.DTOs;

public class MethodDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public Guid ClassId { get; set; }
    public bool IsAbstract { get; set; }
    public bool IsSealed { get; set; }
    public bool IsVirtual { get; set; }
    public bool IsOverride { get; set; }
    public bool IsStatic { get; set; }
    public AccessibilityModifier Accessibility { get; set; }
    public List<ParamModel> Params { get; set; } = new();
    public List<LocalVarModel> Vars { get; set; } = new();
    public List<MethodCallModel> MethodsCalled { get; set; } = new();
    public ClassModel Class { get; set; } = null!;
}
