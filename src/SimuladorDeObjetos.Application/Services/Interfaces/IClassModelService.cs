using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IClassModelService
{
    IEnumerable<ClassModel> GetAll();
    void Add(ClassModel model);
    void Delete(ClassModel model);
    void Update(ClassModel model);
    void SaveChanges();
}
