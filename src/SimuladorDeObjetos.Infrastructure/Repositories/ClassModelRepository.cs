using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class ClassModelRepository : IClassModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public ClassModelRepository(SimuladorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ClassModel classModel)
    {
        ArgumentNullException.ThrowIfNull(classModel);
        _dbContext.Set<ClassModel>().Add(classModel);
        _dbContext.SaveChanges();
    }

    public IEnumerable<ClassModel> GetAll()
    {
        return _dbContext.Set<ClassModel>().Include(c => c.Attributes).ToList();
    }

    public void Delete(ClassModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.Set<ClassModel>().Remove(model);
        _dbContext.SaveChanges();
    }

    public void Update(ClassModel classModel)
    {
        ArgumentNullException.ThrowIfNull(classModel);
        _dbContext.Set<ClassModel>().Update(classModel);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public ClassModel? GetById(Guid id)
    {
        return _dbContext.Set<ClassModel>().Find(id);
    }
}
