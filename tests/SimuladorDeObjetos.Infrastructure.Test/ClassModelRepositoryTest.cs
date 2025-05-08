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
public class ClassModelRepositoryTest
{
    private SimuladorDbContext _context = null!;
    private ClassModelRepository _repo = null!;
    private ClassModel _class = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SimuladorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SimuladorDbContext(options);
        _repo = new ClassModelRepository(_context);

        _class = new ClassModel
        {
            Id = Guid.NewGuid(),
            Name = "MyClass",
            IsAbstract = false,
            IsSealed = false,
            Attributes = new List<AttributeModel>
            {
                new AttributeModel
                {
                    Id = Guid.NewGuid(),
                    Name = "attr",
                    Type = "int",
                    Accessibility = AccessibilityModifier.Public
                }
            },
            Methods = new List<MethodModel>
            {
                new MethodModel
                {
                    Id = Guid.NewGuid(),
                    Name = "method",
                    ReturnType = "void",
                    Accessibility = AccessibilityModifier.Private
                }
            }
        };
    }

    [TestMethod]
    public void Add_ShouldAddClass()
    {
        _repo.Add(_class);
        Assert.AreEqual(1, _context.Classes.Count());
    }

    [TestMethod]
    public void GetAll_ShouldReturnAllWithIncludes()
    {
        _context.Classes.Add(_class);
        _context.SaveChanges();

        var result = _repo.GetAll().ToList();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("MyClass", result[0].Name);
        Assert.AreEqual(1, result[0].Attributes.Count);
        Assert.AreEqual(1, result[0].Methods.Count);
    }

    [TestMethod]
    public void GetById_ShouldReturnClassWithIncludes()
    {
        _context.Classes.Add(_class);
        _context.SaveChanges();

        var found = _repo.GetById(_class.Id);
        Assert.IsNotNull(found);
        Assert.AreEqual("MyClass", found!.Name);
    }

    [TestMethod]
    public void Update_ShouldModifyClass()
    {
        _context.Classes.Add(_class);
        _context.SaveChanges();

        _class.Name = "UpdatedName";
        _repo.Update(_class);

        var result = _context.Classes.First();
        Assert.AreEqual("UpdatedName", result.Name);
    }

    [TestMethod]
    public void Delete_ShouldRemoveClass()
    {
        _context.Classes.Add(_class);
        _context.SaveChanges();

        _repo.Delete(_class);
        Assert.AreEqual(0, _context.Classes.Count());
    }

    [TestMethod]
    public void SaveChanges_ShouldPersist()
    {
        _context.Classes.Add(_class);
        _repo.SaveChanges();
        Assert.AreEqual(1, _context.Classes.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_ShouldThrow_WhenNull()
    {
        _repo.Add(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Update_ShouldThrow_WhenNull()
    {
        _repo.Update(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Delete_ShouldThrow_WhenNull()
    {
        _repo.Delete(null!);
    }
}
