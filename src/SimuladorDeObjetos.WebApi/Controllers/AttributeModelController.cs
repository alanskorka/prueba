using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeModelController : ControllerBase
{
    private readonly IAttributeModelService _service;

    public AttributeModelController(IAttributeModelService service)
    {
        _service = service;
    }
}
