namespace SimuladorDeObjetos.Application.DTOs.Persistence;

public class InterfaceMethodDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public List<ParamDto> Parameters { get; set; } = [];
}
