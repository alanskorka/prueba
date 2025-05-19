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
        var classModel = _classRepo.GetById(attribute.ClassId) ?? throw new Exception("Clase no encontrada");

        if (classModel.IsSealed)
        {
            throw new InvalidOperationException("No se pueden agregar atributos a una clase sellada.");
        }

        if (classModel.Attributes.Any(a => a.Name == attribute.Name))
        {
            throw new InvalidOperationException("Ya existe un atributo con ese nombre en la clase.");
        }

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

        _repo.Update(attribute);
    }

    public void Delete(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _repo.Delete(attribute);
    }
}
