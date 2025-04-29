using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application;

public class ClassModelService : IClassModelService
{
    private readonly IClassModelRepository _repository;

    public ClassModelService(IClassModelRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ClassModel> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add(ClassModel model)
    {
        _repository.Add(model);
    }
}
