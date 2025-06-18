using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NamespacesController(INamespaceService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(service.GetAll());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var ns = service.GetAll().FirstOrDefault(n => n.Id == id);
            if (ns == null) return NotFound();
            return Ok(ns);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Add([FromBody] NamespaceModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            service.Create(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] NamespaceModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            service.Update(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] NamespaceModel model)
    {
        try
        {
            service.Delete(model);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
