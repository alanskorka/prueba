using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class InterfaceMethodModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private InterfaceMethodModelRepository _repository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repository = new InterfaceMethodModelRepository(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public async Task Add_ShouldSaveInterfaceMethodToDatabase()
    {
        var model = new InterfaceMethodModel
        {
            Name = "TestMethod",
            ReturnType = "void"
        };

        await _repository.Add(model);

        var savedModel = await _context.InterfaceMethodModels.FirstOrDefaultAsync();
        Assert.IsNotNull(savedModel);
        Assert.AreEqual("TestMethod", savedModel.Name);
        Assert.AreEqual("void", savedModel.ReturnType);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllInterfaceMethods()
    {
        var methods = new List<InterfaceMethodModel>
        {
            new InterfaceMethodModel
            {
                Name = "Method1",
                ReturnType = "void"
            },
            new InterfaceMethodModel
            {
                Name = "Method2",
                ReturnType = "string"
            }
        };

        await _context.InterfaceMethodModels.AddRangeAsync(methods);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAll();

        var resultList = result.ToList();
        Assert.AreEqual(2, resultList.Count);
        Assert.AreEqual("Method1", resultList[0].Name);
        Assert.AreEqual("Method2", resultList[1].Name);
        Assert.AreEqual("void", resultList[0].ReturnType);
        Assert.AreEqual("string", resultList[1].ReturnType);
    }

    [TestMethod]
    public async Task Update_ShouldModifyExistingInterfaceMethod()
    {
        var existingMethod = new InterfaceMethodModel
        {
            Id = 1,
            Name = "Old",
            ReturnType = "int"
        };
        await _context.InterfaceMethodModels.AddAsync(existingMethod);
        await _context.SaveChangesAsync();

        var updatedMethod = new InterfaceMethodModel
        {
            Id = 1,
            Name = "Updated",
            ReturnType = "string"
        };

        await _repository.Update(updatedMethod);

        var result = await _context.InterfaceMethodModels.FindAsync(1);
        Assert.IsNotNull(result);
        Assert.AreEqual("Updated", result.Name);
        Assert.AreEqual("string", result.ReturnType);
    }
}
