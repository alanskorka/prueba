using System;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class NamespaceModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private NamespaceRepository _repo = null!;
    private NamespaceModel _namespace = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new NamespaceRepository(_context);

        _namespace = new NamespaceModel
        {
            Id = Guid.NewGuid(),
            Name = "TestNamespace"
        };
    }

    [TestMethod]
    public void Add_ShouldAddNamespace()
    {
        _repo.Add(_namespace);
        var result = _context.Namespaces.FirstOrDefault(n => n.Id == _namespace.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual("TestNamespace", result!.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAll()
    {
        _context.Namespaces.Add(_namespace);
        _context.SaveChanges();

        var result = _repo.GetAll();
        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    public void Update_ShouldUpdateNamespace()
    {
        _context.Namespaces.Add(_namespace);
        _context.SaveChanges();

        _namespace.Name = "UpdatedName";
        _repo.Update(_namespace);

        var updated = _context.Namespaces.First();
        Assert.AreEqual("UpdatedName", updated.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldDeleteNamespace()
    {
        _context.Namespaces.Add(_namespace);
        _context.SaveChanges();

        _repo.Delete(_namespace);
        Assert.AreEqual(0, _context.Namespaces.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenNull()
    {
        _repo.Delete(null!);
    }

    [TestMethod]
    public void SaveChanges_ShouldPersistChanges()
    {
        _context.Namespaces.Add(_namespace);
        _repo.SaveChanges();
        Assert.AreEqual(1, _context.Namespaces.Count());
    }

    [TestMethod]
    public void GetById_ShouldReturnCorrectNamespace()
    {
        _context.Namespaces.Add(_namespace);
        _context.SaveChanges();

        var result = _repo.GetById(_namespace.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(_namespace.Name, result!.Name);
    }

    [TestMethod]
    public void GetById_ShouldReturnNull_IfNotFound()
    {
        var result = _repo.GetById(Guid.NewGuid());
        Assert.IsNull(result);
    }
}
