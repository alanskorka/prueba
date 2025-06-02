using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.Services;

public class NamespaceService(INamespaceRepository repository) : INamespaceService
{
    public List<NamespaceModel> GetAll() => repository.GetAll();
    public void Create(NamespaceModel model) => repository.Add(model);
    public void Update(NamespaceModel model) => repository.Update(model);
    public void Delete(NamespaceModel model) => repository.Remove(model);
    public void AddNamespace(NamespaceModel model) => repository.Add(model);

    public void DeleteNamespace(Guid id)
    {
        var ns = repository.GetById(id);
        if (ns != null)
        {
            repository.Remove(ns);
        }
    }

    public NamespaceModel? GetById(Guid id) => repository.GetById(id);
}
