using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

[TestClass]
public class AtributteModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private AtributteModelRepository _repo = null!;
    private AttributeModel _attribute = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new AtributteModelRepository(_context);

        _attribute = new AttributeModel
        {
            Id = Guid.NewGuid(),
            Name = "TestAttr",
            Type = "string",
            ClassId = Guid.NewGuid(),
            Accessibility = AccessibilityModifier.Public
        };
    }

    [TestMethod]
    public void Add_ShouldAddAttribute()
    {
        _repo.Add(_attribute);
        var result = _context.Attributes.FirstOrDefault(a => a.Id == _attribute.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual("TestAttr", result.Name);
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
        _context.Attributes.Add(_attribute);
        _context.SaveChanges();

        var result = _repo.GetAll();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(_attribute.Name, result[0].Name);
    }

    [TestMethod]
    public void Update_ShouldUpdateAttribute()
    {
        _context.Attributes.Add(_attribute);
        _context.SaveChanges();

        _attribute.Name = "Updated";
        _repo.Update(_attribute);

        var updated = _context.Attributes.First();
        Assert.AreEqual("Updated", updated.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldDeleteAttribute()
    {
        _context.Attributes.Add(_attribute);
        _context.SaveChanges();

        _repo.Delete(_attribute);
        Assert.AreEqual(0, _context.Attributes.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenNull()
    {
        _repo.Delete(null!);
    }

    [TestMethod]
    public void SaveChanges_ShouldCallContextSaveChanges()
    {
        _context.Attributes.Add(_attribute);
        _repo.SaveChanges();
        Assert.AreEqual(1, _context.Attributes.Count());
    }

    [TestMethod]
    public void GetById_ShouldReturnAttribute_WhenExists()
    {
        _context.Attributes.Add(_attribute);
        _context.SaveChanges();

        var result = _repo.GetById(_attribute.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(_attribute.Id, result!.Id);
        Assert.AreEqual(_attribute.Name, result.Name);
    }
}
