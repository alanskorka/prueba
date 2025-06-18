using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IInterfaceMethodModelService
{
    Task Add(InterfaceMethodModel model);
    Task<IEnumerable<InterfaceMethodModel>> GetAll();
    Task<InterfaceMethodModel?> GetById(int id);
    Task Update(InterfaceMethodModel model);
    Task Delete(int id);
}
