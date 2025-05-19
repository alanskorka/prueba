using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;
public interface IInterfaceModelRepository
{
    Task Add(InterfaceModel model);
    Task<List<InterfaceModel>> GetAll();
    Task<InterfaceModel?> GetById(int id);
    Task Delete(int id);
    Task<bool> IsUsedByAnyClass(int interfaceId);
    Task Update(InterfaceModel model);
}
