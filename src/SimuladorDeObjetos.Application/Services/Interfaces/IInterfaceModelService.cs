using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IInterfaceModelService
{
    Task Add(InterfaceModel model);
    Task<IEnumerable<InterfaceModel>> GetAll();
    Task<InterfaceModel> GetById(Guid id);
    Task Delete(InterfaceModel id);
    Task Update(InterfaceModel model);
}
