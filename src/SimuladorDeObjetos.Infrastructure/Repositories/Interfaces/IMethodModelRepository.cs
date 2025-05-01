using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IMethodModelRepository
{
    void Add(MethodModel method);
    List<MethodModel> GetAll();
    void Delete(MethodModel method);
    void Update(MethodModel method);
    void SaveChanges();
}
