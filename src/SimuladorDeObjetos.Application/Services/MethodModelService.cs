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

        var lines = new List<string>();

        lines.Add($"{req.ConcreteTypeId}.{method.Name}()");

        foreach (var call in _methodRepo.GetMethodCalls(method.Id))
        {
            AppendCall(lines, call, 1);
        }

        return new SimulationResponse { Lines = lines };
    }

    private void AppendCall(List<string> outLines, MethodCallModel call, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);

        var prefix = call.ReferenceType switch {
            ReferenceTypeInvocation.This => "this",
            ReferenceTypeInvocation.Base => "base",
            ReferenceTypeInvocation.Attribute => $"obj_{call.ReferenceName}",
            ReferenceTypeInvocation.Parameter => $"param_{call.ReferenceName}",
            ReferenceTypeInvocation.LocalVar => $"var_{call.ReferenceName}",
            _ => call.ReferenceType.ToString()
        };
        outLines.Add($"{indent}{prefix}.{call.MethodName}()");

        foreach (var nested in _methodRepo.GetMethodCalls(call.Id))
        {
            AppendCall(outLines, nested, indentLevel + 1);
        }
    }
}
