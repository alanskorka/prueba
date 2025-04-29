using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class AttributeModelService : IAttributeModelService
{
    private readonly IRepositoryAtributteModel _repository;

    public AttributeModelService(IRepositoryAtributteModel repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Create(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _repository.Add(attribute);
    }

    public List<AttributeModel> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _repository.Update(attribute);
    }

    public void Delete(AttributeModel attribute)
    {
        throw new NotImplementedException();
    }
}
