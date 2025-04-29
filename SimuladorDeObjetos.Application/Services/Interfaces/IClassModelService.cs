using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IClassModelService
{
    IEnumerable<ClassModel> GetAll();
}
