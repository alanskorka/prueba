using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ParamModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Guid MethodId { get; set; }
    public Guid? ConcreteTypeId { get; set; }

    [JsonIgnore]
    public MethodModel? Method { get; set; } = null!;

    [JsonIgnore]
    public ClassModel? ConcreteType { get; set; }
}
