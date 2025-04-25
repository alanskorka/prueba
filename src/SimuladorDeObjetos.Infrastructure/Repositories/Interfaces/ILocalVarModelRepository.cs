using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface ILocalVarModelRepository
{
    IEnumerable<LocalVarModel> GetAll();
}
