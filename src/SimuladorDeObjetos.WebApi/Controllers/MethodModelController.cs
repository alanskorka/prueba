using Domain.Entities;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] MethodModel method)
    {
        _service.Add(method);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] MethodModel method)
    {
        _service.Update(method);
        return Ok();
    }
}
