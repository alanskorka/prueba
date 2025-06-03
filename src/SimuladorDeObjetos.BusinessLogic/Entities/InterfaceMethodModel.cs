namespace Domain.Entities;

public class InterfaceMethodModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public List<ParamModel> Parameters { get; set; } = new();
}
