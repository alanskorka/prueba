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
        throw new NotImplementedException();
    }

    public List<AttributeModel> GetAll()
        => throw new NotImplementedException();

    public void Update(AttributeModel attribute)
    {
        throw new NotImplementedException();
    }

    public void Delete(AttributeModel attribute)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
        => throw new NotImplementedException();
}
