using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IAttributeModelService
{
    void Create(AttributeModel attribute);
    List<AttributeModel> GetAll();
    void Update(AttributeModel attribute);
    void Delete(AttributeModel attribute);
}
