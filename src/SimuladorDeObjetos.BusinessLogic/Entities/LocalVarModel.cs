namespace Domain.Entities;

public class LocalVarModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Guid MethodId { get; set; }
    public MethodModel Method { get; set; } = null!;
}
