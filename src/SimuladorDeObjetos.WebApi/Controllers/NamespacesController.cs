using Domain.Entities;
using SimuladorDeObjetos.Application;

namespace SimuladorDeObjetos.WebApi.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NamespacesController(NamespaceService service) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(NamespaceModel model)
    {
        service.AddNamespace(model);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        service.DeleteNamespace(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var ns = service.GetById(id);
        return ns != null ? Ok(ns) : NotFound();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(service.GetAll());
    }
}
