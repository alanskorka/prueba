using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class MethodModelRepository : IMethodModelRepository
{
    private readonly SimuladorDbContext _dbContext;

    public MethodModelRepository(SimuladorDbContext dbContext) => _dbContext = dbContext;

    public void Add(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _dbContext.Methods.Add(method);
        _dbContext.SaveChanges();
    }

    public List<MethodModel> GetAll() => _dbContext.Methods
        .Include(m => m.MethodsCalled)
        .ToList();

    public MethodModel? GetById(Guid id) => _dbContext.Methods
        .Include(m => m.MethodsCalled)
        .FirstOrDefault(m => m.Id == id);

    public IEnumerable<MethodCallModel> GetMethodCalls(Guid methodId) =>
        _dbContext.MethodCalls
        .Where(c => c.ParentMethodId == methodId)
        .ToList();

    public void Update(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _dbContext.Methods.Update(method);
        _dbContext.SaveChanges();
    }

    public void Delete(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _dbContext.Methods.Remove(method);
        _dbContext.SaveChanges();
    }

    public void SaveChanges() => _dbContext.SaveChanges();
}
