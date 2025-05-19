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
public class ParamModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private ParamModelRepository _repo = null!;
    private ParamModel _param = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new ParamModelRepository(_context);

        _param = new ParamModel
        {
            Id = Guid.NewGuid(),
            Name = "param1",
            Type = "int",
            MethodId = Guid.NewGuid()
        };
    }

    [TestMethod]
    public void Add_ShouldInsertParam()
    {
        _repo.Add(_param);
        Assert.AreEqual(1, _context.Params.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllParams()
    {
        _context.Params.Add(_param);
        _context.SaveChanges();

        var list = _repo.GetAll();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("param1", list[0].Name);
    }

    [TestMethod]
    public void Update_ShouldModifyParam()
    {
        _context.Params.Add(_param);
        _context.SaveChanges();

        _param.Name = "Updated";
        _repo.Update(_param);

        Assert.AreEqual("Updated", _context.Params.First().Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldRemoveParam()
    {
        _context.Params.Add(_param);
        _context.SaveChanges();

        _repo.Delete(_param);
        Assert.AreEqual(0, _context.Params.Count());
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
        _context.Params.Add(_param);
        _repo.SaveChanges();

        Assert.AreEqual(1, _context.Params.Count());
    }

    [TestMethod]
    public void GetById_ShouldReturnParam_WhenExists()
    {
        _context.Params.Add(_param);
        _context.SaveChanges();

        var result = _repo.GetById(_param.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(_param.Id, result!.Id);
        Assert.AreEqual(_param.Name, result.Name);
    }
}
