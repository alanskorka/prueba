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
    public AccessibilityModifier Accessibility { get; set; }

    public List<ParamDto> Params { get; set; } = new();
    public List<LocalVarDto> Vars { get; set; } = new();
    public List<MethodCallDto> MethodsCalled { get; set; } = new();
}
