using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class ParamModelService : IParamModelService
{
    private readonly IParamModelRepository _repo;
    private readonly IMethodModelRepository _methodRepo;

    public ParamModelService(IParamModelRepository repo, IMethodModelRepository methodRepo)
    {
        _repo = repo;
        _methodRepo = methodRepo;
    }

    public IEnumerable<ParamModel> GetAll() => _repo.GetAll();

    public void Add(ParamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        var method = _methodRepo.GetById(model.MethodId) ?? throw new Exception("Método no encontrado");

        if (method.Params.Any(p => p.Name == model.Name))
        {
            throw new InvalidOperationException("Ya existe un parámetro con ese nombre en el método.");
        }

        _repo.Add(model);
        _repo.SaveChanges();
    }

    public void Update(ParamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _repo.Update(model);
        _repo.SaveChanges();
    }

    public void Delete(ParamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _repo.Delete(model);
        _repo.SaveChanges();
    }

    public void SaveChanges() => _repo.SaveChanges();
}
