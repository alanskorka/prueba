using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IInterfaceModelService
{
    Task Add(InterfaceModel model);
    Task<IEnumerable<InterfaceModel>> GetAll();
    Task<InterfaceModel> GetById(int id);
    Task Delete(InterfaceModel model);
    Task Update(InterfaceModel model);
}
