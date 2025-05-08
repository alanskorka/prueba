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

    public IEnumerable<MethodModel> GetAll() => _methodRepo.GetAll() ?? new List<MethodModel>();

    public void Add(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var classModel = _classRepo.GetById(method.ClassId) ?? throw new Exception("Clase no encontrada");

        if (classModel.IsSealed)
        {
            throw new InvalidOperationException("No se pueden agregar métodos a una clase sellada.");
        }

        if (classModel.Methods.Any(m => m.Name == method.Name))
        {
            throw new InvalidOperationException("Ya existe un método con ese nombre en la clase.");
        }

        if (method.IsAbstract && !classModel.IsAbstract)
        {
            throw new InvalidOperationException("No se puede agregar un método abstracto a una clase concreta. Marcar la clase como abstracta.");
        }

        _methodRepo.Add(method);
    }

    public void Update(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methodRepo.Update(method);
    }

    public void Delete(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _methodRepo.Delete(method);
    }

    public void AddMethodToClass(Guid classId, MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var classModel = _classRepo.GetById(classId) ?? throw new Exception("Class not found");

        if (method.IsAbstract && !classModel.IsAbstract)
        {
            throw new InvalidOperationException("No se puede agregar un método abstracto a una clase concreta. Marcar la clase como abstracta.");
        }

        if (classModel.IsSealed)
        {
            throw new InvalidOperationException("No se pueden agregar métodos a una clase sellada.");
        }

        if (classModel.Methods.Any(m => m.Name == method.Name))
        {
            throw new InvalidOperationException("Ya existe un método con ese nombre en la clase.");
        }

        classModel.Methods.Add(method);
        _classRepo.Update(classModel);
    }

    public MethodModel? GetByName(Guid classId, string methodName)
    {
        return _methodRepo.GetAll().FirstOrDefault(m => m.ClassId == classId && m.Name == methodName);
    }

    public SimulationResponse SimulateMethodExecution(SimulationRequest req)
    {
        var method = _methodRepo.GetById(req.MethodId) ?? throw new ArgumentException("Método no encontrado");
        var lines = new List<string> { $"{req.ConcreteTypeId}.{method.Name}()" };

        foreach (var call in _methodRepo.GetMethodCalls(method.Id))
        {
            AppendCall(lines, call, 1);
        }

        return new SimulationResponse { Lines = lines };
    }

    private void AppendCall(List<string> lines, MethodCallModel call, int level)
    {
        var indent = new string(' ', level * 2);
        var prefix = call.ReferenceType switch
        {
            ReferenceTypeInvocation.This => "this",
            ReferenceTypeInvocation.Base => "base",
            ReferenceTypeInvocation.Attribute => $"obj_{call.ReferenceName}",
            ReferenceTypeInvocation.Parameter => $"param_{call.ReferenceName}",
            ReferenceTypeInvocation.LocalVar => $"var_{call.ReferenceName}",
            _ => call.ReferenceType.ToString()
        };
        lines.Add($"{indent}{prefix}.{call.MethodName}()");
        foreach (var nested in _methodRepo.GetMethodCalls(call.Id))
        {
            AppendCall(lines, nested, level + 1);
        }
    }
}
