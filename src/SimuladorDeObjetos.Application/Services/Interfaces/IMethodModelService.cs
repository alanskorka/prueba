using Domain.Entities;
using SimuladorDeObjetos.Application.DTOs.Api;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IMethodModelService
{
    IEnumerable<MethodModel> GetAll();
    MethodModel? GetById(Guid id);
    void Add(MethodModel method);
    void Update(MethodModel method);
    void Delete(MethodModel method);
    void AddMethodToClass(Guid classId, MethodModel method);
    SimulationResponse SimulateMethodExecution(SimulationRequest req);
}
