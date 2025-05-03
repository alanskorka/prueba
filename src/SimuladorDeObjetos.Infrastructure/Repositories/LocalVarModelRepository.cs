using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class LocalVarModelRepository : ILocalVarModelRepository
{
    private readonly DbContext _dbContext;

    public LocalVarModelRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(LocalVarModel model)
    {
        _dbContext.Set<LocalVarModel>().Add(model);
    }

    public void Delete(LocalVarModel model)
    {
        _dbContext.Set<LocalVarModel>().Remove(model);
    }

    public void Update(LocalVarModel model)
    {
        _dbContext.Entry(model).State = EntityState.Modified;
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
