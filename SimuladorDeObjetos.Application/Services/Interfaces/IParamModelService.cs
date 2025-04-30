using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IParamModelService
{
    IEnumerable<ParamModel> GetAll();
    void Add(ParamModel model);
    void Update(ParamModel model);
    void Delete(ParamModel model);
}
