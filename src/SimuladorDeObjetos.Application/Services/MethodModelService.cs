using Domain.Entities;
using Domain.Enums;
using SimuladorDeObjetos.Application.DTOs.Api;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class MethodModelService : IMethodModelService
{
    private readonly IMethodModelRepository _methodRepo;
    private readonly IClassModelRepository _classRepo;

    public MethodModelService(IMethodModelRepository methodRepo, IClassModelRepository classRepo)
    {
        _methodRepo = methodRepo;
        _classRepo = classRepo;
    }

    public IEnumerable<MethodModel> GetAll()
    {
        var result = _methodRepo.GetAll();
        return result ?? new List<MethodModel>();
    }

    public void Add(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methodRepo.Add(method);
        _methodRepo.SaveChanges();
    }

    public void Update(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methodRepo.Update(method);
        _methodRepo.SaveChanges();
    }

    public void Delete(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methodRepo.Delete(method);
        _methodRepo.SaveChanges();
    }

    public void AddMethodToClass(Guid classId, MethodModel method)
    {
        if (classId == Guid.Empty)
        {
            throw new ArgumentException("classId is empty");
        }

        ArgumentNullException.ThrowIfNull(method);

        var classModel = _classRepo.GetById(classId);
        if (classModel == null)
        {
            throw new Exception("Class not found");
        }

        classModel.Methods ??= new List<MethodModel>();
        classModel.Methods.Add(method);

        _classRepo.Update(classModel);
        _classRepo.SaveChanges();
    }

    public MethodModel? GetByName(Guid classId, string methodName)
    {
        return _methodRepo.GetAll()
            .FirstOrDefault(m => m.ClassId == classId && m.Name == methodName);
    }

    public SimulationResponse SimulateMethodExecution(SimulationRequest req)
    {
        var method = _methodRepo.GetById(req.MethodId)
            ?? throw new ArgumentException("Método no encontrado");

        return null;
    }

    private void AppendCall(List<string> outLines, MethodCallModel call, int indentLevel)
    {
        throw new NotImplementedException();
    }
}
