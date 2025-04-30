using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class MethodCallModelService : IMethodCallModelService
{
    private readonly IRepositoryMethodCallModel _repository;

    public MethodCallModelService(IRepositoryMethodCallModel repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Create(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _repository.Add(call);
    }

    public List<MethodCallModel> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _repository.Update(call);
    }

    public void Delete(MethodCallModel call)
    {
        _repository.Delete(call);
    }
}
