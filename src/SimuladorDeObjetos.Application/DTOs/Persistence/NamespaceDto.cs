using System;
using System.Collections.Generic;

namespace SimuladorDeObjetos.Application.DTOs;

public abstract class NamespaceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<NamespaceDto> Children { get; set; } = new();
    public NamespaceDto? Parent { get; set; }
}
