using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface ILocalVarModelRepository
{
    void Add(LocalVarModel model);
    void Delete(LocalVarModel model);
    void Update(LocalVarModel model);
    IEnumerable<LocalVarModel> GetAll();
}
