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

    public async Task<bool> IsUsedByAnyClass(int interfaceId)
    {
        return await _dbContext.Classes
            .AnyAsync(c => c.ImplementedInterfaces != null && c.ImplementedInterfaces.Any(i => i.Id == interfaceId));
    }

    public async Task Delete(int id)
    {
        var toDelete = await _dbContext.InterfaceModels
            .Include(i => i.Methods)
            .ThenInclude(m => m.Parameters)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (toDelete != null)
        {
            _dbContext.InterfaceModels.Remove(toDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Update(InterfaceModel model)
    {
        var existingModel = await _dbContext.InterfaceModels
            .Include(i => i.Methods)
            .ThenInclude(m => m.Parameters)
            .FirstOrDefaultAsync(i => i.Id == model.Id);

        if (existingModel == null)
        {
            throw new KeyNotFoundException($"No se encontró un InterfaceModel con el ID {model.Id}");
        }

        existingModel.Name = model.Name;

        _dbContext.InterfaceMethodModels.RemoveRange(existingModel.Methods);

        existingModel.Methods = model.Methods.Select(m => new InterfaceMethodModel
        {
            Name = m.Name,
            ReturnType = m.ReturnType,
        }).ToList();

        await _dbContext.SaveChangesAsync();
    }
}
