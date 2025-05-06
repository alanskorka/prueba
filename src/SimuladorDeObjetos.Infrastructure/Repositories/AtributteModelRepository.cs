using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class AtributteModelRepository : IAtributteModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public AtributteModelRepository(SimuladorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _dbContext.Set<AttributeModel>().Add(attribute);
        _dbContext.SaveChanges();
    }

    public List<AttributeModel> GetAll()
        => _dbContext.Set<AttributeModel>().ToList();

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _dbContext.Set<AttributeModel>().Update(attribute);
        _dbContext.SaveChanges();
    }

    public void Delete(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _dbContext.Set<AttributeModel>().Remove(attribute);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
        => _dbContext.SaveChanges();
}
