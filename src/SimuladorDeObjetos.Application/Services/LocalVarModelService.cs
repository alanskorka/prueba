using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class LocalVarModelService : ILocalVarModelService
{
    private readonly ILocalVarModelRepository _repo;
    private readonly IMethodModelRepository _methodRepo;

    public LocalVarModelService(ILocalVarModelRepository repo, IMethodModelRepository methodRepo)
    {
        _repo = repo;
        _methodRepo = methodRepo;
    }

    public IEnumerable<LocalVarModel> GetAll() => _repo.GetAll();

    public void Add(LocalVarModel model)
    {
        ValidateModelNotNull(model);

        var method = GetMethodOrThrow(model.MethodId);
        ThrowIfNameExists(method.Vars, model.Name ?? throw new InvalidOperationException());

        _repo.Add(model);
        _repo.SaveChanges();
    }

    public void Update(LocalVarModel model)
    {
        ValidateModelNotNull(model);

        ThrowIfNotExists(model.Id);
        var method = GetMethodOrThrow(model.MethodId);
        ThrowIfNameExists(method.Vars, model.Name ?? throw new InvalidOperationException(), model.Id);

        _repo.Update(model);
        _repo.SaveChanges();
    }

    public void Delete(LocalVarModel model)
    {
        ValidateModelNotNull(model);
        ThrowIfNotExists(model.Id);

        _repo.Delete(model);
        _repo.SaveChanges();
    }

    private void ValidateModelNotNull(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
    }

    private void ThrowIfNotExists(Guid id)
    {
        if (_repo.GetById(id) == null)
        {
            throw new InvalidOperationException("Variable local no encontrada.");
        }
    }

    private MethodModel GetMethodOrThrow(Guid methodId)
    {
        return _methodRepo.GetById(methodId)
               ?? throw new InvalidOperationException("Método no encontrado.");
    }

    private void ThrowIfNameExists(IEnumerable<LocalVarModel> vars, string name, Guid? excludeId = null)
    {
        var duplicated = vars.Any(v => v.Name == name && (!excludeId.HasValue || v.Id != excludeId.Value));
        if (duplicated)
        {
            throw new InvalidOperationException("Ya existe otra variable local con ese nombre en el método.");
        }
    }
}
