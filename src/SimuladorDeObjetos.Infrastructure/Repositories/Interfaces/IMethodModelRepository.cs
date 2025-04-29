using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IMethodModelRepository
{
    MethodModel GetById(Guid id);
    IEnumerable<MethodModel> GetByClassId(Guid classId);
    void Add(MethodModel method);
    void Delete(MethodModel method);
    void Update(MethodModel method);
    void SaveChanges();
}
