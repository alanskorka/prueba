using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IMethodCallModelService
{
    void Create(MethodCallModel call);
    List<MethodCallModel> GetAll();
    MethodCallModel? GetById(Guid id);
    void Update(MethodCallModel call);
    void Delete(MethodCallModel call);
}
