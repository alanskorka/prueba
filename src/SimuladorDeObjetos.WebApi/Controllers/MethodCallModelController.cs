using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MethodCallModelController : ControllerBase
{
    private readonly IMethodCallModelService _service;

    public MethodCallModelController(IMethodCallModelService service)
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
    public IActionResult Create([FromBody] MethodCallModel model)
    {
        _service.Create(model);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] MethodCallModel model)
    {
        _service.Update(model);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] MethodCallModel model)
    {
        _service.Delete(model);
        return Ok();
    }
}
