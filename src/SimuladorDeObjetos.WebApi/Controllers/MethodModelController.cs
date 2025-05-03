using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MethodModelController : ControllerBase
{
    private readonly IMethodModelService _service;

    public MethodModelController(IMethodModelService service)
    {
        _service = service;
    }
}
