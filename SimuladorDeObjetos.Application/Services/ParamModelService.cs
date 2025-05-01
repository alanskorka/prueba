using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class ParamModelService : IParamModelService
{
    private readonly IParamModelRepository _paramModelRepository;

    public ParamModelService(IParamModelRepository paramModelRepository)
    {
        _paramModelRepository = paramModelRepository;
    }

    public IEnumerable<ParamModel> GetAll()
    {
        return _paramModelRepository.GetAll();
    }

    public void Add(ParamModel model)
    {
        _paramModelRepository.Add(model);
        _paramModelRepository.SaveChanges();
    }

    public void Update(ParamModel model)
    {
        _paramModelRepository.Update(model);
        _paramModelRepository.SaveChanges();
    }

    public void Delete(ParamModel model)
    {
        _paramModelRepository.Delete(model);
        _paramModelRepository.SaveChanges();
    }

    public void SaveChanges()
    {
        _paramModelRepository.SaveChanges();
    }
}
