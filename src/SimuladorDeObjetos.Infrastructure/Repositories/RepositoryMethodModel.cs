using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class RepositoryMethodModel
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

    public void Delete(MethodModel method) => throw new NotImplementedException();
    public void SaveChanges() => throw new NotImplementedException();
}
