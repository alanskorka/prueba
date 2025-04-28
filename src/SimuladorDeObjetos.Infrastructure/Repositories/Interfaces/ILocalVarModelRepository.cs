using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface ILocalVarModelRepository
{
    void Add(LocalVarModel model);
    IEnumerable<LocalVarModel> GetAll();
}
