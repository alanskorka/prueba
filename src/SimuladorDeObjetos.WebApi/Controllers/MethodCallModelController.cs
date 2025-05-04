using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimuladorDeObjetos.Application.DTOs;
using SimuladorDeObjetos.Application.Interfaces;

namespace SimuladorDeObjetos.WebApi.Controllers;

[ApiController]
[Route("api/methodcalls")]
public class MethodCallModelController : ControllerBase
{
    private readonly IMethodCallModelService _service;
    private readonly IMapper _mapper;

    public MethodCallModelController(IMethodCallModelService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<List<MethodCallDto>> GetAll()
    {
        var models = _service.GetAll();
        var dtos = _mapper.Map<List<MethodCallDto>>(models);
        return Ok(dtos);
    }

    [HttpPost]
    public IActionResult Create([FromBody] MethodCallDto dto)
    {
        var model = _mapper.Map<MethodCallModel>(dto);
        _service.Create(model);
        var createdDto = _mapper.Map<MethodCallDto>(model);
        return CreatedAtAction(nameof(GetAll), null, createdDto);
    }

    [HttpPut]
    public IActionResult Update([FromBody] MethodCallDto dto)
    {
        var model = _mapper.Map<MethodCallModel>(dto);
        _service.Update(model);
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] MethodCallDto dto)
    {
        var model = _mapper.Map<MethodCallModel>(dto);
        _service.Delete(model);
        return NoContent();
    }
}
