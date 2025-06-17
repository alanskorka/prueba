using Domain.Entities;
using Domain.Enums;

namespace SimuladorDeObjetos.Application.DTOs.Persistence;

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
    public List<ParamModel> Params { get; set; } = [];
    public List<LocalVarModel> Vars { get; set; } = [];
    public List<MethodCallModel> MethodsCalled { get; set; } = [];
    public ClassModel Class { get; set; } = null!;
}
