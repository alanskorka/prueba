using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class AtributteModelRepository : IAtributteModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public AtributteModelRepository(SimuladorDbContext dbContext) => _dbContext = dbContext;

    public void Add(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _dbContext.Attributes.Add(attribute);
        _dbContext.SaveChanges();
    }

    public List<AttributeModel> GetAll() => _dbContext.Attributes.ToList();

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _dbContext.Attributes.Update(attribute);
        _dbContext.SaveChanges();
    }

    public void Delete(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _dbContext.Attributes.Remove(attribute);
        _dbContext.SaveChanges();
    }

    public void SaveChanges() => _dbContext.SaveChanges();
}
