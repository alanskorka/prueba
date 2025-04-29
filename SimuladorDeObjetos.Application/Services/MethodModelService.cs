using Domain.Entities;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Application
{
    public class MethodModelService : IMethodModelService
    {
        private readonly IMethodModelRepository _methodRepo;
        private readonly IClassModelRepository _classRepo;

        public MethodModelService(IMethodModelRepository methodRepo, IClassModelRepository classRepo)
        {
            _methodRepo = methodRepo;
            _classRepo = classRepo;
        }

        public void AddMethodToClass(Guid classId, MethodModel method)
        {
            var classModel = _classRepo.GetById(classId);
            if (classModel == null)
            {
                throw new Exception("Class not found");
            }

            // Acá seguís con la lógica de agregar el método
            classModel.Methods.Add(method);

            _classRepo.Update(classModel);
            _classRepo.SaveChanges();
        }
    }
}
