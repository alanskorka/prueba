using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class RepositoryAtributteModel : IRepositoryAtributteModel
{
    private readonly DbContext _dbContext;
    private readonly DbSet<AttributeModel> _attributes;

    public RepositoryAtributteModel(DbContext dbContext)
    {
        _dbContext = dbContext;
        _attributes = dbContext.Set<AttributeModel>();
    }

    public void Add(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _attributes.Add(attribute);
        _dbContext.SaveChanges();
    }

    public List<AttributeModel> GetAll()
        => _attributes.ToList();

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _attributes.Update(attribute);
        _dbContext.SaveChanges();
    }

    public void Delete(AttributeModel attribute)
    {
        _attributes.Remove(attribute);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
        => throw new NotImplementedException();
}
