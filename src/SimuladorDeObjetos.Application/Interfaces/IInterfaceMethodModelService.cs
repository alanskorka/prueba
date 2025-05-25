using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IInterfaceMethodModelService
{
    Task Add(InterfaceMethodModel model);
    Task<IEnumerable<InterfaceMethodModel>> GetAll();
    Task Update(InterfaceMethodModel model);
}