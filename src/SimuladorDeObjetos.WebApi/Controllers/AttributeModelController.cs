using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Application.Services.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeModelController : ControllerBase
{
    private readonly IAttributeModelService _service;

    public AttributeModelController(IAttributeModelService service) => _service = service;

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
    public IActionResult Add([FromBody] AttributeModel model)
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
    public IActionResult Update([FromBody] AttributeModel model)
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
    public IActionResult Delete([FromBody] AttributeModel model)
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
