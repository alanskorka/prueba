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

        if (_repo.GetAll().Any(c => c.Name == model.Name))
        {
            throw new InvalidOperationException($"Ya existe una clase con el nombre '{model.Name}'.");
        }

        if (model.BaseClassId.HasValue)
        {
            var baseClass = _repo.GetById(model.BaseClassId.Value);
            if (baseClass == null)
            {
                throw new InvalidOperationException("Clase base no encontrada.");
            }

            if (baseClass.IsSealed)
            {
                throw new InvalidOperationException("No se puede heredar de una clase sellada.");
            }
        }

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

        var sameName = _repo.GetAll()
            .Any(c => c.Name == model.Name && c.Id != model.Id);
        if (sameName)
        {
            throw new InvalidOperationException($"Ya existe otra clase con el nombre '{model.Name}'.");
        }

        if (model.BaseClassId.HasValue)
        {
            var baseClass = _repo.GetById(model.BaseClassId.Value);
            if (baseClass == null)
            {
                throw new InvalidOperationException("Clase base no encontrada.");
            }
        }

        _repo.Update(model);
        _repo.SaveChanges();
    }

    public void SaveChanges() => _repo.SaveChanges();

    public ClassModel? GetByName(string name) => _repo.GetAll().FirstOrDefault(c => c.Name == name);
}
