using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class AttributeModelService : IAttributeModelService
{
    private readonly IAtributteModelRepository _atributteModelRepository;

    public AttributeModelService(IAtributteModelRepository atributteModelRepository)
    {
        _atributteModelRepository = atributteModelRepository ?? throw new ArgumentNullException(nameof(atributteModelRepository));
    }

    public void Create(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _atributteModelRepository.Add(attribute);
    }

    public List<AttributeModel> GetAll()
    {
        return _atributteModelRepository.GetAll();
    }

    public void Update(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _atributteModelRepository.Update(attribute);
    }

    public void Delete(AttributeModel attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);
        _atributteModelRepository.Delete(attribute);
    }
}
