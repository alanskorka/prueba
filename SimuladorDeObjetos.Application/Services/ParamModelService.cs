using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class ParamModelService : IParamModelService
{
    private readonly IRepositoryParamModel _repository;

    public ParamModelService(IRepositoryParamModel repository)
    {
        _repository = repository;
    }

    public IEnumerable<ParamModel> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add(ParamModel model)
    {
        _repository.Add(model);
        _repository.SaveChanges();
    }

    public void Update(ParamModel model)
    {
        _repository.Update(model);
        _repository.SaveChanges();
    }

    public void Delete(ParamModel model)
    {
        _repository.Delete(model);
        _repository.SaveChanges();
    }

    public void SaveChanges()
    {
        _repository.SaveChanges();
    }
}
