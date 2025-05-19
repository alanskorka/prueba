using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<InterfaceModel>> GetAll()
    {
        return await _dbContext.InterfaceModels
            .Include(i => i.Methods)
            .ThenInclude(m => m.Parameters)
            .ToListAsync();
    }

    public async Task<InterfaceModel?> GetById(int id)
    {
        return await _dbContext.InterfaceModels
            .Include(i => i.Methods)
            .ThenInclude(m => m.Parameters)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
