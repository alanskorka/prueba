using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class MethodCallModelRepository : IMethodCallModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public MethodCallModelRepository(SimuladorDbContext dbContext) => _dbContext = dbContext;

    public void Add(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _dbContext.MethodCalls.Add(call);
        _dbContext.SaveChanges();
    }

    public List<MethodCallModel> GetAll() => _dbContext.MethodCalls.ToList();

    public void Update(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _dbContext.MethodCalls.Update(call);
        _dbContext.SaveChanges();
    }

    public void Delete(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _dbContext.MethodCalls.Remove(call);
        _dbContext.SaveChanges();
    }

    public void SaveChanges() => _dbContext.SaveChanges();
}
