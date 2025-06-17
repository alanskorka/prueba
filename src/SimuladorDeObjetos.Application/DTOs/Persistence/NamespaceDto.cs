namespace SimuladorDeObjetos.Application.DTOs.Persistence;

public abstract class NamespaceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<NamespaceDto> Children { get; set; } = [];
    public NamespaceDto? Parent { get; set; }
}
