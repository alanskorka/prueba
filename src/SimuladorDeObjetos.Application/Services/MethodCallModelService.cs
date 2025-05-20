using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class MethodCallModelService : IMethodCallModelService
{
    private readonly IMethodCallModelRepository _repo;
    private const string NotFoundMessage = "Llamada de método no encontrada.";
    private const string DuplicateIdMessage = "Ya existe una llamada de método con ese ID.";

    public MethodCallModelService(IMethodCallModelRepository repo)
    {
        _repo = repo;
    }

    public void Create(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        ThrowIfExists(call.Id, DuplicateIdMessage);
        _repo.Add(call);
    }

    public List<MethodCallModel> GetAll() => _repo.GetAll();

    public void Update(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        ThrowIfNotExists(call.Id);
        _repo.Update(call);
    }

    public void Delete(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        ThrowIfNotExists(call.Id);
        _repo.Delete(call);
    }

    private void ThrowIfNotExists(Guid id)
    {
        if (_repo.GetById(id) == null)
        {
            throw new InvalidOperationException(NotFoundMessage);
        }
    }

    private void ThrowIfExists(Guid id, string message)
    {
        if (_repo.GetById(id) != null)
        {
            throw new InvalidOperationException(message);
        }
    }
}
