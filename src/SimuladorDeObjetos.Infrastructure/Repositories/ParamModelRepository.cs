using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class ParamModelRepository : IParamModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public ParamModelRepository(SimuladorDbContext dbContext) => _dbContext = dbContext;

    public void Add(ParamModel param)
    {
        ArgumentNullException.ThrowIfNull(param);
        _dbContext.Params.Add(param);
        _dbContext.SaveChanges();
    }

    public List<ParamModel> GetAll() => _dbContext.Params.ToList();

    public void Update(ParamModel param)
    {
        ArgumentNullException.ThrowIfNull(param);
        _dbContext.Params.Update(param);
        _dbContext.SaveChanges();
    }

    public void Delete(ParamModel param)
    {
        ArgumentNullException.ThrowIfNull(param);
        _dbContext.Params.Remove(param);
        _dbContext.SaveChanges();
    }

    public void SaveChanges() => _dbContext.SaveChanges();

    public ParamModel? GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
