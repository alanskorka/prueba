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
        throw new NotImplementedException();
    }

    public void Delete(ParamModel param)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
        => throw new NotImplementedException();
}
