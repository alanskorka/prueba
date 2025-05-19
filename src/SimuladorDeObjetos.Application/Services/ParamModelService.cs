using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class ParamModelService : IParamModelService
{
    private readonly IParamModelRepository _repo;
    private readonly IMethodModelRepository _methodRepo;

    private const string ParamNotFoundMsg = "Parámetro no encontrado.";
    private const string MethodNotFoundMsg = "Método no encontrado.";
    private const string DuplicateParamMsg = "Ya existe otro parámetro con ese nombre en el método.";

    public ParamModelService(IParamModelRepository repo, IMethodModelRepository methodRepo)
    {
        _repo = repo;
        _methodRepo = methodRepo;
    }

    public IEnumerable<ParamModel> GetAll() => _repo.GetAll();

    public void Add(ParamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var method = FindMethodOrThrow(model.MethodId);

        ValidateUniqueName(method.Params, model.Name ?? throw new InvalidOperationException());

        _repo.Add(model);
        _repo.SaveChanges();
    }

    public void Update(ParamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (_repo.GetById(model.Id) == null)
        {
            throw new InvalidOperationException(ParamNotFoundMsg);
        }

        var method = FindMethodOrThrow(model.MethodId);

        ValidateUniqueName(method.Params, model.Name ?? throw new InvalidOperationException(), model.Id);

        _repo.Update(model);
        _repo.SaveChanges();
    }

    public void Delete(ParamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (_repo.GetById(model.Id) == null)
        {
            throw new InvalidOperationException(ParamNotFoundMsg);
        }

        _repo.Delete(model);
        _repo.SaveChanges();
    }

    public void SaveChanges() => _repo.SaveChanges();

    private MethodModel FindMethodOrThrow(Guid methodId)
    {
        return _methodRepo.GetById(methodId) ?? throw new InvalidOperationException(MethodNotFoundMsg);
    }

    private void ValidateUniqueName(IEnumerable<ParamModel> parameters, string name, Guid? excludeId = null)
    {
        var exists = parameters.Any(p => p.Name == name && (!excludeId.HasValue || p.Id != excludeId.Value));
        if (exists)
        {
            throw new InvalidOperationException(DuplicateParamMsg);
        }
    }
}
