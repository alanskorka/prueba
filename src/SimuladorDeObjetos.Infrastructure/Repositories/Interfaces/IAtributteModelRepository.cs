using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IAtributteModelRepository
{
    void Add(AttributeModel attribute);
    List<AttributeModel> GetAll();
    void Update(AttributeModel attribute);
    void Delete(AttributeModel attribute);
    void SaveChanges();
    AttributeModel? GetById(Guid id);
}
