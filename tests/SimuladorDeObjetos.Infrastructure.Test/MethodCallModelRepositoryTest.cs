using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

    [TestClass]
public class MethodCallModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private MethodCallModelRepository _repo = null!;
    private MethodCallModel _call = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new MethodCallModelRepository(_context);

        _call = new MethodCallModel
        {
            Id = Guid.NewGuid(),
            MethodName = "DoSomething",
            ReferenceType = ReferenceTypeInvocation.This,
            ReferenceName = "this",
            ParentMethodId = Guid.NewGuid()
        };
    }

    [TestMethod]
    public void Add_ShouldInsertCall()
    {
        _repo.Add(_call);
        Assert.AreEqual(1, _context.MethodCalls.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllCalls()
    {
        _context.MethodCalls.Add(_call);
        _context.SaveChanges();

        var result = _repo.GetAll();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("DoSomething", result[0].MethodName);
    }

    [TestMethod]
    public void Update_ShouldModifyCall()
    {
        _context.MethodCalls.Add(_call);
        _context.SaveChanges();

        _call.MethodName = "Updated";
        _repo.Update(_call);

        Assert.AreEqual("Updated", _context.MethodCalls.First().MethodName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldRemoveCall()
    {
        _context.MethodCalls.Add(_call);
        _context.SaveChanges();

        _repo.Delete(_call);
        Assert.AreEqual(0, _context.MethodCalls.Count());
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
        _context.MethodCalls.Add(_call);
        _repo.SaveChanges();

        Assert.AreEqual(1, _context.MethodCalls.Count());
    }

    [TestMethod]
    public void GetById_ShouldReturnCorrectCall_WhenExists()
    {
        var call = new MethodCallModel
        {
            Id = Guid.NewGuid(),
            MethodName = "TestCall",
            ReferenceType = ReferenceTypeInvocation.This,
            ParentMethodId = Guid.NewGuid(),
            ReferenceName = "this"
        };

        _context.MethodCalls.Add(call);
        _context.SaveChanges();

        var repo = new MethodCallModelRepository(_context);
        var result = repo.GetById(call.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(call.Id, result!.Id);
    }
}
