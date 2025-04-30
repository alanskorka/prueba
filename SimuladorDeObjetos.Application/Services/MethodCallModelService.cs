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
        throw new NotImplementedException();
    }

    public void Update(MethodCallModel call)
    {
        throw new NotImplementedException();
    }

    public void Delete(MethodCallModel call)
    {
        throw new NotImplementedException();
    }
}
