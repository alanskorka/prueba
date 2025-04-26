using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class RepositoryParamModel : IRepositoryParamModel
{
    private readonly DbContext _dbContext;
    private readonly DbSet<ParamModel> _params;

    public RepositoryParamModel(DbContext dbContext)
    {
        _dbContext = dbContext;
        _params = dbContext.Set<ParamModel>();
    }

    public void Add(ParamModel param)
    {
        ArgumentNullException.ThrowIfNull(param);
        _params.Add(param);
        _dbContext.SaveChanges();
    }

    public List<ParamModel> GetAll()
        => _params.ToList();

    public void Update(ParamModel param)
    {
        ArgumentNullException.ThrowIfNull(param);
        _params.Update(param);
        _dbContext.SaveChanges();
    }

    public void Delete(ParamModel param)
    {
        ArgumentNullException.ThrowIfNull(param);
        _params.Remove(param);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
        => _dbContext.SaveChanges();
}
