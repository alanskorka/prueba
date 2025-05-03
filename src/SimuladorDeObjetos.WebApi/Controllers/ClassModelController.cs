using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassModelController : ControllerBase
{
    private readonly IClassModelService _service;

    public ClassModelController(IClassModelService service)
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
    public IActionResult Add([FromBody] ClassModel model)
    {
        _service.Add(model);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] ClassModel model)
    {
        _service.Update(model);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ClassModel model)
    {
        _service.Delete(model);
        return Ok();
    }
}
