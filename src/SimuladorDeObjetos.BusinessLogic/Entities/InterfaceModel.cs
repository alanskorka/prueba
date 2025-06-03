namespace Domain.Entities;

public class InterfaceModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<InterfaceMethodModel> Methods { get; set; } = new();
}
