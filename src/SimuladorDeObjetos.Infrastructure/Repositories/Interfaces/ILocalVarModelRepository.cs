using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface ILocalVarModelRepository
{
    void Add(LocalVarModel model);
    void Delete(LocalVarModel model);
    IEnumerable<LocalVarModel> GetAll();
}
