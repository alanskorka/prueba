using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IMethodCallModelRepository
{
    void Add(MethodCallModel call);
    List<MethodCallModel> GetAll();
    void Update(MethodCallModel call);
    void Delete(MethodCallModel call);
    void SaveChanges();
    MethodCallModel? GetById(Guid id);
}
