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
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult Create([FromBody] MethodCallDto dto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public IActionResult Update([FromBody] MethodCallDto dto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] MethodCallDto dto)
    {
        throw new NotImplementedException();
    }
}
