using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces
{
    public interface IMethodModelService
    {
        IEnumerable<MethodModel> GetAll();
        void Add(MethodModel method);
        void Update(MethodModel method);
        void Delete(MethodModel method);
        void AddMethodToClass(Guid classId, MethodModel method);
    }
}
