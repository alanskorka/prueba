namespace SimuladorDeObjetos.Application.DTOs.Persistence;

public class InterfaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<InterfaceMethodDto> Methods { get; set; } = [];
}
