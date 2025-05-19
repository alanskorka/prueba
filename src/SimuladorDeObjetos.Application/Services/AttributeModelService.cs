using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class AttributeModelService : IAttributeModelService
{
    private readonly IAtributteModelRepository _repo;
    private readonly IClassModelRepository _classRepo;

    public AttributeModelService(IAtributteModelRepository repo, IClassModelRepository classRepo)
    {
        _repo = repo;
        _classRepo = classRepo;
    }

    public void Create(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);

        var classModel = GetClassModelOrThrow(attribute.ClassId);

        if (classModel.IsSealed)
        {
            throw new InvalidOperationException("No se pueden agregar atributos a una clase sellada.");
        }

        ValidateDuplicateName(classModel.Attributes, attribute.Name);

        _repo.Add(attribute);
    }

    public List<AttributeModel> GetAll() => _repo.GetAll();

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);

        var existing = _repo.GetById(attribute.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Atributo no encontrado.");
        }

        var classModel = GetClassModelOrThrow(attribute.ClassId);

        if (classModel.IsSealed)
        {
            throw new InvalidOperationException("No se pueden modificar atributos en una clase sellada.");
        }

        ValidateDuplicateName(classModel.Attributes, attribute.Name, attribute.Id);

        _repo.Update(attribute);
    }

    public void Delete(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _repo.Delete(attribute);
    }

    private ClassModel GetClassModelOrThrow(Guid classId)
    {
        return _classRepo.GetById(classId) ?? throw new InvalidOperationException("Clase no encontrada.");
    }

    private void ValidateDuplicateName(IEnumerable<AttributeModel> attributes, string name, Guid? excludeId = null)
    {
        var exists = attributes.Any(a => a.Name == name && (!excludeId.HasValue || a.Id != excludeId));
        if (exists)
        {
            throw new InvalidOperationException($"Ya existe {(excludeId != null ? "otro " : string.Empty)}atributo con ese nombre en la clase.");
        }
    }
}
