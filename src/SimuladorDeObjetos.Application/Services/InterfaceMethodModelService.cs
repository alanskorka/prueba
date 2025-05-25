using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class InterfaceMethodModelService : IInterfaceMethodModelService
{
    private readonly IInterfaceMethodModelRepository _repository;

    public InterfaceMethodModelService(IInterfaceMethodModelRepository repository)
    {
        _repository = repository;
    }

    public async Task Add(InterfaceMethodModel model)
    {
        await _repository.Add(model);
    }

    public async Task<IEnumerable<InterfaceMethodModel>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task Update(InterfaceMethodModel model)
    {
         await _repository.Update(model);
    }
}
