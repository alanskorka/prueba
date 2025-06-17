using Domain.Entities;
using Domain.Enums;

namespace SimuladorDeObjetos.Application.DTOs.Persistence;

public class MethodCallDto
{
    public Guid Id { get; set; }
    public string? MethodName { get; set; }
    public ReferenceTypeInvocation ReferenceType { get; set; }
    public Guid ParentMethodId { get; set; }
    public string? ReferenceName { get; set; }
    public MethodModel ParentMethod { get; set; } = null!;
    public Guid? ParentCallId { get; set; }
    public List<Guid> ConcreteParameterTypes { get; set; } = [];
    public List<ClassModel> ConcreteParameters { get; set; } = [];
}
