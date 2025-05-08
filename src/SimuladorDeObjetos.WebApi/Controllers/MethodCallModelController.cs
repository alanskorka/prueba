using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MethodCallModelController : ControllerBase
{
    private readonly IMethodCallModelService _service;

    public MethodCallModelController(IMethodCallModelService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_service.GetAll());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] MethodCallModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _service.Create(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] MethodCallModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _service.Update(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] MethodCallModel model)
    {
        try
        {
            _service.Delete(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
