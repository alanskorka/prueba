using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IParamModelRepository
{
    void Add(ParamModel param);
    List<ParamModel> GetAll();
    void Update(ParamModel param);
    void Delete(ParamModel param);
    void SaveChanges();
    ParamModel? GetById(Guid id);
}
