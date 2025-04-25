using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class LocalVarModelRepository
{
    private readonly DbContext _dbContext;

    public LocalVarModelRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<LocalVarModel> GetAll()
    {
        return _dbContext.Set<LocalVarModel>().ToList();
    }
}
