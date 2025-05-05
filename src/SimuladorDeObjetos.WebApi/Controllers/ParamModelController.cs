using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParamModelController : ControllerBase
{
    private readonly IParamModelService _service;

    public ParamModelController(IParamModelService service)
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
    public IActionResult Add([FromBody] ParamModel param)
    {
        _service.Add(param);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] ParamModel param)
    {
        _service.Update(param);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ParamModel param)
    {
        _service.Delete(param);
        return Ok();
    }
}
