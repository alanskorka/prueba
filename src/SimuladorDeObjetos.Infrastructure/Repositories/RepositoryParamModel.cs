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
        throw new NotImplementedException();
    }

    public List<ParamModel> GetAll()
        => throw new NotImplementedException();

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
