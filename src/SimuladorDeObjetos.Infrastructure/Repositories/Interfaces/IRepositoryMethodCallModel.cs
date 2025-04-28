using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IRepositoryMethodCallModel
{
    void Add(MethodCallModel call);
    List<MethodCallModel> GetAll();
    void Update(MethodCallModel call);
    void Delete(MethodCallModel call);
    void SaveChanges();
}
