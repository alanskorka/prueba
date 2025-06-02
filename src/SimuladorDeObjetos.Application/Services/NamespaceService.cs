using Domain.Entities;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class NamespaceService(INamespaceRepository repo)
{
    public void AddNamespace(NamespaceModel model)
    {
        repo.Add(model);
    }

    public void DeleteNamespace(Guid id)
    {
        var ns = repo.GetById(id);
        if (ns != null)
        {
            repo.Remove(ns);
        }
    }

    public NamespaceModel? GetById(Guid id) => repo.GetById(id);
    public List<NamespaceModel> GetAll() => repo.GetAll();
}
