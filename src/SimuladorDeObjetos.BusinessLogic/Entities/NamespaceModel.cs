using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class NamespaceModel
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;

    public NamespaceModel? Parent { get; private set; }

    public List<NamespaceModel> Children { get; private set; } = new();

    public void AddChild(NamespaceModel child)
    {
        ArgumentNullException.ThrowIfNull(child);

        child.Parent = this;
        Children.Add(child);
    }
}
