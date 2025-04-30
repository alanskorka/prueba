using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IParamModelService
{
    IEnumerable<ParamModel> GetAll();
    void Add(ParamModel model);
}
