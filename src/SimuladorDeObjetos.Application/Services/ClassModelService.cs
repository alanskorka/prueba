using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class ClassModelService : IClassModelService
{
    private readonly IClassModelRepository _repo;
    public ClassModelService(IClassModelRepository repo) => _repo = repo;

    public IEnumerable<ClassModel> GetAll() => _repo.GetAll();

    public void Add(ClassModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        ValidateUniqueName(model.Name ?? throw new InvalidOperationException());
        ValidateBaseClass(model.BaseClassId);

        _repo.Add(model);
    }

    public void Delete(ClassModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _repo.Delete(model);
    }

    public void Update(ClassModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var existing = _repo.GetById(model.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("La clase a actualizar no existe.");
        }

        ValidateUniqueName(model.Name ?? throw new InvalidOperationException(), model.Id);
        ValidateBaseClass(model.BaseClassId);

        _repo.Update(model);
        _repo.SaveChanges();
    }

    public void SaveChanges() => _repo.SaveChanges();

    public ClassModel? GetByName(string name) =>
        _repo.GetAll().FirstOrDefault(c => c.Name == name);

    private void ValidateUniqueName(string name, Guid? currentId = null)
    {
        var exists = _repo.GetAll()
            .Any(c => c.Name == name && (!currentId.HasValue || c.Id != currentId));
        if (exists)
        {
            throw new InvalidOperationException($"Ya existe una clase con el nombre '{name}'.");
        }
    }

    private void ValidateBaseClass(Guid? baseClassId)
    {
        if (!baseClassId.HasValue)
        {
            return;
        }

        var baseClass = _repo.GetById(baseClassId.Value);
        if (baseClass == null)
        {
            throw new InvalidOperationException("Clase base no encontrada.");
        }

        if (baseClass.IsSealed)
        {
            throw new InvalidOperationException("No se puede heredar de una clase sellada.");
        }
    }
}
