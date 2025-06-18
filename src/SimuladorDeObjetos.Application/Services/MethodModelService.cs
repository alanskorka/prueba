using Domain.Entities;
using Domain.Enums;
using SimuladorDeObjetos.Application.DTOs.Api;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Application.Services.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application.Services;

public class MethodModelService(IMethodModelRepository methodRepo, IClassModelRepository classRepo) : IMethodModelService
{
    private readonly IMethodModelRepository _methodRepo = methodRepo;
    private readonly IClassModelRepository _classRepo = classRepo;

    public IEnumerable<MethodModel> GetAll() => _methodRepo.GetAll() ?? Enumerable.Empty<MethodModel>();

    public void Add(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var classModel = GetClassOrThrow(method.ClassId);
        ValidateAddOrUpdate(method, classModel);

        _methodRepo.Add(method);
    }

    public void Update(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var existing = _methodRepo.GetById(method.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Método no encontrado.");
        }

        var classModel = GetClassOrThrow(method.ClassId);
        ValidateAddOrUpdate(method, classModel, isUpdate: true);

        _methodRepo.Update(method);
    }

    public void Delete(MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var existing = _methodRepo.GetById(method.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Método no encontrado.");
        }

        _methodRepo.Delete(method);
    }

    public void AddMethodToClass(Guid classId, MethodModel method)
    {
        ArgumentNullException.ThrowIfNull(method);

        var classModel = GetClassOrThrow(classId);
        ValidateAddOrUpdate(method, classModel);

        classModel.Methods.Add(method);
        _classRepo.Update(classModel);
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

        var classModel = _classRepo.GetById(method.ClassId)
            ?? throw new InvalidOperationException("Clase asociada al método no encontrada.");

        var className = classModel.Name;
        var lines = new List<string> { $"{className}.{method.Name}()" };

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
            ReferenceTypeInvocation.Attribute => GetReferenceWithType("obj", call.ReferenceName),
            ReferenceTypeInvocation.Parameter => GetReferenceWithType("param", call.ReferenceName),
            ReferenceTypeInvocation.LocalVar => GetReferenceWithType("var", call.ReferenceName),
            _ => call.ReferenceType.ToString()
        };

        if (call.MethodName != null)
        {
            var methodToExecute = GetMethodToExecute(call.MethodName, GetBaseTypeId(), GetConcreteTypeId());
            var methodName = methodToExecute?.Name ?? call.MethodName;

            lines.Add($"{indent}{prefix}.{methodName}()");
        }

        foreach (var nested in _methodRepo.GetMethodCalls(call.Id))
        {
            AppendCall(lines, nested, level + 1);
        }
    }

    private string GetReferenceWithType(string prefix, string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return prefix;
        }

        return $"{prefix}_{name}";
    }

    private Guid GetBaseTypeId()
    {
        // Aquí deberíamos obtener el tipo base de la referencia según el ReferenceType
        // Por ahora retornamos un valor por defecto
        return Guid.Empty;
    }

    private Guid GetConcreteTypeId()
    {
        // Aquí deberíamos obtener el tipo concreto de la referencia según el ReferenceType
        // Por ahora retornamos un valor por defecto
        return Guid.Empty;
    }

    private ClassModel GetClassOrThrow(Guid classId)
    {
        return _classRepo.GetById(classId)
            ?? throw new InvalidOperationException("Clase no encontrada.");
    }

    private void ValidateAddOrUpdate(MethodModel method, ClassModel classModel, bool isUpdate = false)
    {
        if (method.IsAbstract && !classModel.IsAbstract)
        {
            throw new InvalidOperationException("No se puede agregar un método abstracto a una clase concreta. Marcar la clase como abstracta.");
        }

        if (classModel.IsSealed)
        {
            throw new InvalidOperationException("No se pueden agregar métodos a una clase sellada.");
        }

        var duplicate = classModel.Methods.Any(m => m.Name == method.Name && (!isUpdate || m.Id != method.Id));
        if (duplicate)
        {
            throw new InvalidOperationException("Ya existe un método con ese nombre en la clase.");
        }

        if (method.IsOverride)
        {
            ValidateOverride(method, classModel);
        }
    }

    private void ValidateOverride(MethodModel method, ClassModel derivedClass)
    {
        if (derivedClass.BaseClassId == null)
        {
            throw new InvalidOperationException("No se puede usar override en una clase que no hereda de otra.");
        }

        var baseClass = _classRepo.GetById(derivedClass.BaseClassId.Value);
        if (baseClass == null)
        {
            throw new InvalidOperationException("No se encontró la clase base.");
        }

        var baseMethod = baseClass.Methods.FirstOrDefault(m => m.Name == method.Name);
        if (baseMethod == null)
        {
            throw new InvalidOperationException("No existe un método con el mismo nombre en la clase base para hacer override.");
        }

        if (!baseMethod.IsVirtual)
        {
            throw new InvalidOperationException("El método base debe ser virtual para poder hacer override.");
        }
    }

    public MethodModel? GetMethodToExecute(string methodName, Guid baseClassId, Guid concreteClassId)
    {
        var baseClass = _classRepo.GetById(baseClassId);
        var concreteClass = _classRepo.GetById(concreteClassId);

        if (concreteClass == null)
        {
            throw new InvalidOperationException("Concrete class not found.");
        }

        var overrideMethod = concreteClass.Methods
            .FirstOrDefault(m => m.Name == methodName && m.IsOverride);

        if (overrideMethod != null)
        {
            return overrideMethod;
        }

        if (baseClass == null)
        {
            throw new InvalidOperationException("Base class not found.");
        }

        var virtualMethod = baseClass.Methods
            .FirstOrDefault(m => m.Name == methodName && m.IsVirtual);

        return virtualMethod ?? baseClass.Methods.FirstOrDefault(m => m.Name == methodName);
    }
}
