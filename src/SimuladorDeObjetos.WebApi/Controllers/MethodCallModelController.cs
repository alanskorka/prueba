using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}
