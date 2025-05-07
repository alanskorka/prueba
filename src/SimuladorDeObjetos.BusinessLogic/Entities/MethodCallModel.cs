using Domain.Enums;

namespace Domain.Entities;

public class MethodCallModel
{
    public Guid Id { get; set; }
    public string? MethodName { get; set; }
    public ReferenceTypeInvocation ReferenceType { get; set; }
    public Guid ParentMethodId { get; set; }
    public string? ReferenceName { get; set; }
}
