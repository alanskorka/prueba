using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterfaceModelController : ControllerBase
{
    private readonly IInterfaceModelService _service;

    public InterfaceModelController(IInterfaceModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<InterfaceModel>>> GetAll()
    {
        try
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InterfaceModel>> GetById(int id)
    {
        var result = await _service.GetById(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] InterfaceModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _service.Add(model);
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InterfaceModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _service.Update(model);
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] InterfaceModel model)
    {
        try
        {
            await _service.Delete(model);
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
