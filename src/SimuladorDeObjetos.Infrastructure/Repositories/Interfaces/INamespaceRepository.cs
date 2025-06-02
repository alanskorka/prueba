using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface INamespaceRepository
{
    void Add(NamespaceModel model);
    void Remove(NamespaceModel model);
    NamespaceModel? GetById(Guid id);
    List<NamespaceModel> GetAll();
    void Update(NamespaceModel model);
    void Delete(NamespaceModel model);
    void SaveChanges();
}
