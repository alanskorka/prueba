namespace SimuladorDeObjetos.Application.DTOs;

public class InterfaceMethodDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public List<ParamDto> Parameters { get; set; } = new();
}
