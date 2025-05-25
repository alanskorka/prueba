using System;
using System.Collections.Generic;

namespace SimuladorDeObjetos.Application.DTOs;

public class NamespaceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }
    public List<NamespaceDto> Children { get; set; } = new();
}
