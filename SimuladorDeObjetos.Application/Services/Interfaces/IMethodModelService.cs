using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IMethodModelService
{
    void AddMethodToClass(Guid classId, MethodModel method);
}
