using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

   [TestClass]
public class LocalVarModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private LocalVarModelRepository _repo = null!;
    private LocalVarModel _var = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new LocalVarModelRepository(_context);

        _var = new LocalVarModel
        {
            Id = Guid.NewGuid(),
            Name = "varX",
            Type = "bool",
            MethodId = Guid.NewGuid()
        };
    }

    [TestMethod]
    public void Add_ShouldInsert()
    {
        _repo.Add(_var);
        Assert.AreEqual(1, _context.LocalVars.Count());
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
        _context.LocalVars.Add(_var);
        _context.SaveChanges();

        var list = _repo.GetAll().ToList();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("varX", list[0].Name);
    }

    [TestMethod]
    public void Update_ShouldModify()
    {
        _context.LocalVars.Add(_var);
        _context.SaveChanges();

        _var.Name = "Updated";
        _repo.Update(_var);

        Assert.AreEqual("Updated", _context.LocalVars.First().Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldRemove()
    {
        _context.LocalVars.Add(_var);
        _context.SaveChanges();

        _repo.Delete(_var);
        Assert.AreEqual(0, _context.LocalVars.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenNull()
    {
        _repo.Delete(null!);
    }

    [TestMethod]
    public void SaveChanges_ShouldCommit()
    {
        _context.LocalVars.Add(_var);
        _repo.SaveChanges();
        Assert.AreEqual(1, _context.LocalVars.Count());
    }

    [TestMethod]
    public void GetById_ShouldReturnCorrectLocalVar()
    {
        _context.LocalVars.Add(_var);
        _context.SaveChanges();

        var result = _repo.GetById(_var.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual(_var.Id, result.Id);
    }

    [TestMethod]
    public void GetById_ShouldReturnNull_WhenNotFound()
    {
        var result = _repo.GetById(Guid.NewGuid());
        Assert.IsNull(result);
    }
}
