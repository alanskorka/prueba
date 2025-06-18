using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IInterfaceMethodModelRepository
{
    Task Add(InterfaceMethodModel model);
    Task<IEnumerable<InterfaceMethodModel>> GetAll();
    Task<InterfaceMethodModel?> GetById(int id);
    Task Update(InterfaceMethodModel model);
    Task Delete(int id);
}
