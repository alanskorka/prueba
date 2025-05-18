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
        if (model.Methods.GroupBy(m => m.Name).Any(g => g.Count() > 1))
        {
            throw new ArgumentException("Duplicate method names are not allowed.");
        }

        await _repository.Add(model);
    }

    public async Task<List<InterfaceModel>> GetAll()
    {
        return await _repository.GetAll();
    }
}
