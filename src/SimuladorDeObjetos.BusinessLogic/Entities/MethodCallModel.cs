namespace Domain.Entities;

public class MethodCallModel
{
    public string? MethodName { get; set; }
    public ReferenceTypeInvocation ReferenceType { get; set; }
}
