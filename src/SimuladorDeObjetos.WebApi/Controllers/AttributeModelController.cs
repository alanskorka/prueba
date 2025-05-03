using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeModelController : ControllerBase
{
    private readonly IAttributeModelService _service;

    public AttributeModelController(IAttributeModelService service)
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
    public IActionResult Add([FromBody] AttributeModel model)
    {
        _service.Create(model);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] AttributeModel model)
    {
        _service.Update(model);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] AttributeModel model)
    {
        _service.Delete(model);
        return Ok();
    }
}
