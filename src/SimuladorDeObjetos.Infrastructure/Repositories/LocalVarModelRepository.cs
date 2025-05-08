using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class LocalVarModelRepository : ILocalVarModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public LocalVarModelRepository(SimuladorDbContext dbContext) => _dbContext = dbContext;

    public void Add(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.LocalVars.Add(model);
        _dbContext.SaveChanges();
    }

    public IEnumerable<LocalVarModel> GetAll() => _dbContext.LocalVars.ToList();

    public void Update(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.LocalVars.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(LocalVarModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.LocalVars.Remove(model);
        _dbContext.SaveChanges();
    }

    public void SaveChanges() => _dbContext.SaveChanges();
}
