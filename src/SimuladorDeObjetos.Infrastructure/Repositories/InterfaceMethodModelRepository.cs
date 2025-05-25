using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class InterfaceMethodModelRepository : IInterfaceMethodModelRepository
{
    private readonly SimuladorDbContext _context;

    public InterfaceMethodModelRepository(SimuladorDbContext context)
    {
        _context = context;
    }

    public async Task Add(InterfaceMethodModel model)
    {
        _context.InterfaceMethodModels.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<InterfaceMethodModel>> GetAll()
    {
        return await _context.InterfaceMethodModels.ToListAsync();
    }

    public async Task Update(InterfaceMethodModel model)
    {
        var existingMethod = await _context.InterfaceMethodModels.FindAsync(model.Id);
        if(existingMethod == null)
        {
            throw new KeyNotFoundException($"InterfaceMethodModel with Id {model.Id} not found");
        }

        existingMethod.Name = model.Name;
        existingMethod.ReturnType = model.ReturnType;
        existingMethod.Parameters = model.Parameters;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var method = await _context.InterfaceMethodModels.FindAsync(id);
        if(method != null)
        {
            _context.InterfaceMethodModels.Remove(method);
            await _context.SaveChangesAsync();
        }
    }
}
