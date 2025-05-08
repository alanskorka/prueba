using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.DTOs.Api;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MethodModelController : ControllerBase
{
    private readonly IMethodModelService _service;

    public MethodModelController(IMethodModelService service) => _service = service;

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
    public IActionResult Add([FromBody] MethodModel method)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _service.Add(method);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] MethodModel method)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _service.Update(method);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] MethodModel method)
    {
        try
        {
            _service.Delete(method);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost("add-to-class")]
    public IActionResult AddToClass(Guid classId, [FromBody] MethodModel method)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _service.AddMethodToClass(classId, method);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost("simulate")]
    public IActionResult Simulate([FromBody] SimulationRequest req)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var resp = _service.SimulateMethodExecution(req);
            return Ok(resp);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
