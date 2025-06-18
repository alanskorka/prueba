using Domain.Entities;

namespace SimuladorDeObjetos.Application.Interfaces;

public interface IParamModelService
{
    IEnumerable<ParamModel> GetAll();
    ParamModel? GetById(Guid id);
    void Add(ParamModel model);
    void Update(ParamModel model);
    void Delete(ParamModel model);
    void SaveChanges();
}
