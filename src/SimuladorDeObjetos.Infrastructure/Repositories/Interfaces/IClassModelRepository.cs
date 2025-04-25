using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IClassModelRepository
{
    void Add(ClassModel model);
    IEnumerable<ClassModel> GetAll();
    void Delete(ClassModel model);
}
