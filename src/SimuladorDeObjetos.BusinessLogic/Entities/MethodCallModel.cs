using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class MethodCallModel
{
    public Guid Id { get; set; }
    public string? MethodName { get; set; }
    public ReferenceTypeInvocation ReferenceType { get; set; } = ReferenceTypeInvocation.This;
    public string? ReferenceName { get; set; }
    public Guid? ParentMethodId { get; set; }
    public List<Guid> ConcreteParameterTypes { get; set; } = new();

    [JsonIgnore]
    public MethodModel? ParentMethod { get; set; }

    [JsonIgnore]
    public List<ClassModel> ConcreteParameters { get; set; } = new();
}
