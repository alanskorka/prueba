using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface INamespaceService
{
    List<NamespaceModel> GetAll();
    void Create(NamespaceModel model);
    void Update(NamespaceModel model);
    void Delete(NamespaceModel model);
}
