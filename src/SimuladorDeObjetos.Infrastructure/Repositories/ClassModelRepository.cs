using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class ClassModelRepository : IClassModelRepository
{
    private readonly DbContext _dbContext;

    public ClassModelRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ClassModel classModel)
    {
        _dbContext.Add(classModel);
    }

    public IEnumerable<ClassModel> GetAll()
    {
        return _dbContext.Set<ClassModel>().ToList();
    }

    public void Delete(ClassModel model)
    {
        _dbContext.Set<ClassModel>().Remove(model);
    }

    public void Update(ClassModel classModel)
    {
        _dbContext.Entry(classModel).State = EntityState.Modified;
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
