using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IRepositoryMethodModel
{
    void Add(MethodModel method);
    List<MethodModel> GetAll();
    void Delete(MethodModel method);
    void SaveChanges();
}
