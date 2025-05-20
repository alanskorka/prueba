using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class MethodCallModelService : IMethodCallModelService
{
    private readonly IMethodCallModelRepository _repo;
    public MethodCallModelService(IMethodCallModelRepository repo) => _repo = repo;

    public void Create(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        if (_repo.GetById(call.Id) != null)
        {
            throw new InvalidOperationException("Ya existe una llamada de método con ese ID.");
        }

        _repo.Add(call);
    }

    public List<MethodCallModel> GetAll() => _repo.GetAll();

    public void Update(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        var existing = _repo.GetById(call.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Llamada de método no encontrada.");
        }

        _repo.Update(call);
    }

    public void Delete(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _repo.Delete(call);
    }
}
