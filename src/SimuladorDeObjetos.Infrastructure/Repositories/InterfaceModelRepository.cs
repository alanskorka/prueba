using Domain.Entities;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class InterfaceModelRepository : IInterfaceModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public InterfaceModelRepository(SimuladorDbContext context)
    {
        _dbContext = context;
    }

    public async Task Add(InterfaceModel model)
    {
        _dbContext.InterfaceModels.Add(model);
        await _dbContext.SaveChangesAsync();
    }
}
