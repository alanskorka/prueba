using Domain.Entities;
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
        return _methodRepo.GetAll();
    }

    public void Add(MethodModel method)
    {
        _methodRepo.Add(method);
        _methodRepo.SaveChanges();
    }

    public void Update(MethodModel method)
    {
        _methodRepo.Update(method);
        _methodRepo.SaveChanges();
    }

    public void Delete(MethodModel method)
    {
        _methodRepo.Delete(method);
        _methodRepo.SaveChanges();
    }

    public void AddMethodToClass(Guid classId, MethodModel method)
    {
        var classModel = _classRepo.GetById(classId);
        if (classModel == null)
        {
            throw new Exception("Class not found");
        }

        classModel.Methods.Add(method);
        _classRepo.Update(classModel);
        _classRepo.SaveChanges();
    }
}
