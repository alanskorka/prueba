using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class ClassModelRepository : IClassModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public ClassModelRepository(SimuladorDbContext dbContext) => _dbContext = dbContext;

    public void Add(ClassModel classModel)
    {
        ArgumentNullException.ThrowIfNull(classModel);
        _dbContext.Classes.Add(classModel);
        _dbContext.SaveChanges();
    }

    public IEnumerable<ClassModel> GetAll() => _dbContext.Classes
        .Include(c => c.Attributes)
        .Include(c => c.Methods)
        .ToList();

    public ClassModel? GetById(Guid id) => _dbContext.Classes
        .Include(c => c.Attributes)
        .Include(c => c.Methods)
        .FirstOrDefault(c => c.Id == id);

    public void Update(ClassModel classModel)
    {
        ArgumentNullException.ThrowIfNull(classModel);
        _dbContext.Classes.Update(classModel);
        _dbContext.SaveChanges();
    }

    public void Delete(ClassModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.Classes.Remove(model);
        _dbContext.SaveChanges();
    }

    public void SaveChanges() => _dbContext.SaveChanges();
}
