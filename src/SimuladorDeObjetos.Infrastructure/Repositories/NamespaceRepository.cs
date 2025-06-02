using Domain.Entities;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Repositories;

public class NamespaceRepository(SimuladorDbContext context) : INamespaceRepository
{
    public void Add(NamespaceModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        context.Namespaces.Add(model);
        context.SaveChanges();
    }

    public void Remove(NamespaceModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        context.Namespaces.Remove(model);
        context.SaveChanges();
    }

    public NamespaceModel? GetById(Guid id)
    {
        return context.Namespaces.FirstOrDefault(n => n.Id == id);
    }

    public List<NamespaceModel> GetAll()
    {
        return context.Namespaces.ToList();
    }

    public void Update(NamespaceModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        context.Namespaces.Update(model);
        context.SaveChanges();
    }

    public void Delete(NamespaceModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        context.Namespaces.Remove(model);
        context.SaveChanges();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}
