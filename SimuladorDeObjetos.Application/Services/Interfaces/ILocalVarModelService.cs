using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface ILocalVarModelService
{
    IEnumerable<LocalVarModel> GetAll();
    void Add(LocalVarModel model);
    void Update(LocalVarModel model);
    void Delete(LocalVarModel model);
}
