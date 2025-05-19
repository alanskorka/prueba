using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;
public interface IInterfaceModelRepository
{
    Task Add(InterfaceModel model);
    Task<List<InterfaceModel>> GetAll();
    Task<InterfaceModel?> GetById(int id);
}
