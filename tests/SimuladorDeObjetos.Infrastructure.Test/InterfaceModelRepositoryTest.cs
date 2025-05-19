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
}
