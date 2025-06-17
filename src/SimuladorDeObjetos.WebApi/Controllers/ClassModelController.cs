using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/class")]
public class ClassModelController(IClassModelService service) : ControllerBase
{
    private readonly IClassModelService _service = service;

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
    public IActionResult Add([FromBody] ClassModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _service.Add(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] ClassModel model)
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
    public IActionResult Delete([FromBody] ClassModel model)
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
