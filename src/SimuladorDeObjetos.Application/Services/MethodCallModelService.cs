using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class MethodCallModelService : IMethodCallModelService
{
    private readonly IMethodCallModelRepository _methodCallModelRepository;

    public MethodCallModelService(IMethodCallModelRepository methodCallModelRepository)
    {
        _methodCallModelRepository = methodCallModelRepository ?? throw new ArgumentNullException(nameof(methodCallModelRepository));
    }

    public void Create(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _methodCallModelRepository.Add(call);
    }

    public List<MethodCallModel> GetAll()
    {
        return _methodCallModelRepository.GetAll();
    }

    public void Update(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _methodCallModelRepository.Update(call);
    }

    public void Delete(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _methodCallModelRepository.Delete(call);
    }
}
