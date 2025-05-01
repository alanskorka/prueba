using Domain.Entities;

namespace SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

public interface IRepositoryParamModel
{
    void Add(ParamModel param);
    List<ParamModel> GetAll();
    void Update(ParamModel param);
    void Delete(ParamModel param);
    void SaveChanges();
}
