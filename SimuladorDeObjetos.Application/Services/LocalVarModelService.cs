using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class LocalVarModelService : ILocalVarModelService
{
    private readonly ILocalVarModelRepository _repository;

    public LocalVarModelService(ILocalVarModelRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<LocalVarModel> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add(LocalVarModel model)
    {
        _repository.Add(model);
        _repository.SaveChanges();
    }

    public void Update(LocalVarModel model)
    {
        _repository.Update(model);
        _repository.SaveChanges();
    }

    public void Delete(LocalVarModel model)
    {
        _repository.Delete(model);
        _repository.SaveChanges();
    }
}
