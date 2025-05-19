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
        ArgumentNullException.ThrowIfNull(model);
        var method = _methodRepo.GetById(model.MethodId) ?? throw new Exception("Método no encontrado");

        if (method.Vars.Any(v => v.Name == model.Name))
        {
            throw new InvalidOperationException("Ya existe una variable local con ese nombre en el método.");
        }

        _repo.Add(model);
        _repo.SaveChanges();
    }

    public void Update(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        var existing = _repo.GetById(model.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Variable local no encontrada.");
        }

        var method = _methodRepo.GetById(model.MethodId)
                     ?? throw new InvalidOperationException("Método no encontrado.");

        if (method.Vars.Any(v => v.Name == model.Name && v.Id != model.Id))
        {
            throw new InvalidOperationException("Ya existe otra variable local con ese nombre en el método.");
        }

        _repo.Update(model);
        _repo.SaveChanges();
    }

    public void Delete(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _repo.Delete(model);
        _repo.SaveChanges();
    }
}
