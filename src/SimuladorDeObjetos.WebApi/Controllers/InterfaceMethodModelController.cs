using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterfaceMethodModelController(IInterfaceMethodModelService service) : ControllerBase
{
    private readonly IInterfaceMethodModelService _service = service;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] InterfaceMethodModel model)
    {
        await _service.Add(model);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterfaceMethodModel>>> GetAll()
    {
        var result = await _service.GetAll();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] InterfaceMethodModel model)
    {
        await _service.Update(model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}