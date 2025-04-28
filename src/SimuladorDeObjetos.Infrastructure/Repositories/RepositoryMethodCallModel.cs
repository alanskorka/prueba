using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class RepositoryMethodCallModel : IRepositoryMethodCallModel
{
    private readonly DbContext _dbContext;
    private readonly DbSet<MethodCallModel> _calls;

    public RepositoryMethodCallModel(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _calls = dbContext.Set<MethodCallModel>();
    }

    public void Add(MethodCallModel call)
    {
        ArgumentNullException.ThrowIfNull(call);
        _calls.Add(call);
        _dbContext.SaveChanges();
    }

    public List<MethodCallModel> GetAll()
        => _calls.ToList();

    public void Update(MethodCallModel call)
    {
        _calls.Update(call);
        _dbContext.SaveChanges();
    }

    public void Delete(MethodCallModel call)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
        => throw new NotImplementedException();
}
