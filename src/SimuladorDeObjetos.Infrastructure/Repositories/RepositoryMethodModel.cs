using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class RepositoryMethodModel : IRepositoryMethodModel
{
    private readonly DbContext _dbContext;
    private readonly DbSet<MethodModel> _methods;

    public RepositoryMethodModel(DbContext dbContext)
    {
        _dbContext = dbContext;
        _methods = dbContext.Set<MethodModel>();
    }

    public void Add(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methods.Add(method);
        _dbContext.SaveChanges();
    }

    public List<MethodModel> GetAll()
    {
        return _methods.ToList();
    }

    public void Delete(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methods.Remove(method);
        _dbContext.SaveChanges();
    }

    public void Update(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methods.Update(method);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
