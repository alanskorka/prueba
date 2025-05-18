using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;
public interface IInterfaceModelRepository
{
    Task Add(InterfaceModel model);
}
