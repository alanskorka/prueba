using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class MethodModelRepository : IMethodModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public MethodModelRepository(SimuladorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _dbContext.Set<MethodModel>().Add(method);
        _dbContext.SaveChanges();
    }

    public List<MethodModel> GetAll()
    {
        return _dbContext.Set<MethodModel>().ToList();
    }

    public void Delete(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _dbContext.Set<MethodModel>().Remove(method);
        _dbContext.SaveChanges();
    }

    public void Update(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _dbContext.Set<MethodModel>().Update(method);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
