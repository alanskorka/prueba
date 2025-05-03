namespace SimuladorDeObjetos.Application.DTOs;
using Domain.Enums;
public class MethodCallDto
{
    public string? MethodName { get; set; }
    public ReferenceTypeInvocation ReferenceType { get; set; }
}
