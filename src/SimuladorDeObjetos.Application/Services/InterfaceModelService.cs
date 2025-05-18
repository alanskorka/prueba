using Domain.Entities;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class InterfaceModelService
{
    private readonly IInterfaceModelRepository _repository;

    public InterfaceModelService(IInterfaceModelRepository repository)
    {
        _repository = repository;
    }

    public async Task Add(InterfaceModel model)
    {
        await _repository.Add(model);
    }
}
