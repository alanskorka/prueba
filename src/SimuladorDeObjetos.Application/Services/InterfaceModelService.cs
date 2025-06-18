using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class InterfaceModelService : IInterfaceModelService
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

    public async Task<IEnumerable<InterfaceModel>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<InterfaceModel?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task Delete(InterfaceModel model)
    {
        if (await _repository.IsUsedByAnyClass(model.Id))
        {
            throw new InvalidOperationException("Interface is in use by a class.");
        }

        await _repository.Delete(model.Id);
    }

    public async Task Update(InterfaceModel model)
    {
        await _repository.Update(model);
    }
}
