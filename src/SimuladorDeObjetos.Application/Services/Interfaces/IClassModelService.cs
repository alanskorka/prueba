using Domain.Entities;

namespace SimuladorDeObjetos.Application.Services.Interfaces;

public interface IClassModelService
{
    IEnumerable<ClassModel> GetAll();
    void Add(ClassModel model);
    void Delete(ClassModel model);
    void Update(ClassModel model);
    void SaveChanges();
    ClassModel? GetByName(string name);
}
