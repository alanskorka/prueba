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
public class MethodModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private MethodModelRepository _repo = null!;
    private MethodModel _method = null!;
    private MethodCallModel _call = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new MethodModelRepository(_context);

        _method = new MethodModel
        {
            Id = Guid.NewGuid(),
            Name = "TestMethod",
            ReturnType = "void",
            ClassId = Guid.NewGuid(),
            Accessibility = AccessibilityModifier.Public
        };

        _call = new MethodCallModel
        {
            Id = Guid.NewGuid(),
            MethodName = "CallSomething",
            ParentMethodId = _method.Id,
            ReferenceType = ReferenceTypeInvocation.This,
            ReferenceName = "this"
        };
    }

    [TestMethod]
    public void Add_ShouldInsertMethod()
    {
        _repo.Add(_method);
        Assert.AreEqual(1, _context.Methods.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllMethodsWithCalls()
    {
        _method.MethodsCalled.Add(_call);
        _context.Methods.Add(_method);
        _context.SaveChanges();

        var result = _repo.GetAll();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].MethodsCalled.Count);
    }

    [TestMethod]
    public void GetById_ShouldReturnCorrectMethod()
    {
        _context.Methods.Add(_method);
        _context.SaveChanges();

        var found = _repo.GetById(_method.Id);
        Assert.IsNotNull(found);
        Assert.AreEqual("TestMethod", found!.Name);
    }

    [TestMethod]
    public void GetMethodCalls_ShouldReturnLinkedCalls()
    {
        _context.Methods.Add(_method);
        _context.MethodCalls.Add(_call);
        _context.SaveChanges();

        var result = _repo.GetMethodCalls(_method.Id).ToList();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("CallSomething", result[0].MethodName);
    }

    [TestMethod]
    public void Update_ShouldModifyMethod()
    {
        _context.Methods.Add(_method);
        _context.SaveChanges();

        _method.Name = "Updated";
        _repo.Update(_method);

        Assert.AreEqual("Updated", _context.Methods.First().Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    public void Delete_ShouldRemoveMethod()
    {
        _context.Methods.Add(_method);
        _context.SaveChanges();

        _repo.Delete(_method);
        Assert.AreEqual(0, _context.Methods.Count());
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
        _context.Methods.Add(_method);
        _repo.SaveChanges();

        Assert.AreEqual(1, _context.Methods.Count());
    }
}
