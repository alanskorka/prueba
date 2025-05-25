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
}