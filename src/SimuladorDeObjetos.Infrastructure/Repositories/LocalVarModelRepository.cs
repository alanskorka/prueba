using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class LocalVarModelRepository : ILocalVarModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public LocalVarModelRepository(SimuladorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.Set<LocalVarModel>().Add(model);
        _dbContext.SaveChanges();
    }

    public void Delete(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.Set<LocalVarModel>().Remove(model);
        _dbContext.SaveChanges();
    }

    public void Update(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.Set<LocalVarModel>().Update(model);
        _dbContext.SaveChanges();
    }

    public IEnumerable<LocalVarModel> GetAll()
    {
        return _dbContext.Set<LocalVarModel>().ToList();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
