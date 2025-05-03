using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class LocalVarModelController : ControllerBase
{
    private readonly ILocalVarModelService _service;

    public LocalVarModelController(ILocalVarModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] LocalVarModel model)
    {
        _service.Add(model);
        return Ok();
    }
}
