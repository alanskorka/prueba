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

        var classModel = GetClassOrThrow(attribute.ClassId);

        ThrowIfClassIsSealed(classModel);
        ThrowIfAttributeNameExists(classModel.Attributes, attribute.Name);

        _repo.Add(attribute);
    }

    public List<AttributeModel> GetAll() => _repo.GetAll();

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);

        EnsureAttributeExists(attribute.Id);
        var classModel = GetClassOrThrow(attribute.ClassId);

        ThrowIfClassIsSealed(classModel);
        ThrowIfAttributeNameExists(classModel.Attributes, attribute.Name, attribute.Id);

        _repo.Update(attribute);
    }

    public void Delete(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        EnsureAttributeExists(attribute.Id);
        _repo.Delete(attribute);
    }

    private void EnsureAttributeExists(Guid id)
    {
        if (_repo.GetById(id) == null)
        {
            throw new InvalidOperationException("Atributo no encontrado.");
        }
    }

    private ClassModel GetClassOrThrow(Guid classId)
    {
        return _classRepo.GetById(classId) ?? throw new InvalidOperationException("Clase no encontrada.");
    }

    private void ThrowIfClassIsSealed(ClassModel cls)
    {
        if (cls.IsSealed)
        {
            throw new InvalidOperationException("No se pueden modificar atributos en una clase sellada.");
        }
    }

    private void ThrowIfAttributeNameExists(IEnumerable<AttributeModel> attributes, string name, Guid? excludeId = null)
    {
        var exists = attributes.Any(a => a.Name == name && (!excludeId.HasValue || a.Id != excludeId.Value));
        if (exists)
        {
            throw new InvalidOperationException("Ya existe otro atributo con ese nombre en la clase.");
        }
    }
}
