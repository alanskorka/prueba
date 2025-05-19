using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class InterfaceModelRepositoryTest
{
    private SimuladorDbContext? _context;
    private InterfaceModelRepository? _repository;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repository = new InterfaceModelRepository(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public async Task Add_ShouldSaveInterfaceWithMethods()
    {
        var model = new InterfaceModel
        {
            Name = "ITest",
            Methods = new List<InterfaceMethodModel>
            {
                new() { Name = "Do", ReturnType = "void", Parameters = new() }
            }
        };

        await _repository.Add(model);

        var saved = await _context.InterfaceModels.Include(i => i.Methods).FirstOrDefaultAsync();
        Assert.IsNotNull(saved);
        Assert.AreEqual("ITest", saved.Name);
        Assert.AreEqual(1, saved.Methods.Count);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllInterfaces()
    {
        _context.InterfaceModels.Add(new InterfaceModel { Name = "I1" });
        _context.InterfaceModels.Add(new InterfaceModel { Name = "I2" });

        await _context.SaveChangesAsync();

        var result = await _repository.GetAll();
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnCorrectInterface()
    {
        var model = new InterfaceModel { Name = "IGet" };
        _context.InterfaceModels.Add(model);
        await _context.SaveChangesAsync();

        var result = await _repository.GetById(model.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual("IGet", result!.Name);
    }

    [TestMethod]
    public async Task Delete_ShouldRemoveInterface()
    {
        var model = new InterfaceModel { Name = "IDelete" };
        _context.InterfaceModels.Add(model);
        await _context.SaveChangesAsync();

        await _repository.Delete(model.Id);

        var exists = await _context.InterfaceModels.AnyAsync(i => i.Id == model.Id);
        Assert.IsFalse(exists);
    }

    [TestMethod]
    public async Task IsUsedByAnyClass_ShouldReturnTrue_WhenClassImplementsInterface()
    {
        var iface = new InterfaceModel { Name = "IUsed" };
        var clase = new ClassModel
        {
            Name = "C1",
            ImplementedInterfaces = new List<InterfaceModel> { iface }
        };

        _context.InterfaceModels.Add(iface);
        _context.Classes.Add(clase);
        await _context.SaveChangesAsync();

        var result = await _repository.IsUsedByAnyClass(iface.Id);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task Update_ShouldModifyInterfaceAndReplaceMethods()
    {
        var original = new InterfaceModel
        {
            Name = "IOriginal",
            Methods = new List<InterfaceMethodModel>
            {
                new InterfaceMethodModel
                {
                    Name = "OldMethod",
                    ReturnType = "void",
                    Parameters = new List<ParameterModel>()
                }
            }
        };

        _context.InterfaceModels.Add(original);
        await _context.SaveChangesAsync();

        var updated = new InterfaceModel
        {
            Id = original.Id,
            Name = "IModified",
            Methods = new List<InterfaceMethodModel>
            {
                new InterfaceMethodModel
                {
                    Name = "NewMethod",
                    ReturnType = "int",
                    Parameters = new List<ParameterModel>
                    {
                        new ParameterModel { Name = "x", Type = "int" }
                    }
                }
            }
        };

        await _repository.Update(updated);

        var result = await _context.InterfaceModels
            .Include(i => i.Methods)
            .ThenInclude(m => m.Parameters)
            .FirstOrDefaultAsync(i => i.Id == original.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual("IModified", result!.Name);
        Assert.AreEqual(1, result.Methods.Count);
        Assert.AreEqual("NewMethod", result.Methods.First().Name);
        Assert.AreEqual(1, result.Methods.First().Parameters.Count);
        Assert.AreEqual("x", result.Methods.First().Parameters.First().Name);
    }
}
