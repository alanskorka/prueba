namespace SimuladorDeObjetos.Application.DTOs;

public class InterfaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<InterfaceMethodDto> Methods { get; set; } = new();
}
