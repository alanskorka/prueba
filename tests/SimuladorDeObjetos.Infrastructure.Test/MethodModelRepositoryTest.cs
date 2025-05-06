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
        private MethodModel _method = null!;
        private IQueryable<MethodModel> _data = null!;
        private Mock<DbSet<MethodModel>> _mockSet = null!;
        private Mock<SimuladorDbContext> _mockContext = null!;
        private MethodModelRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _method = new MethodModel
            {
                Id = Guid.NewGuid(),
                Name = "TestMethod",
                ReturnType = "void",
                ClassId = Guid.NewGuid(),
                IsAbstract = false,
                IsSealed = false,
                Accessibility = AccessibilityModifier.Public
            };
            _data = new List<MethodModel> { _method }.AsQueryable();

            _mockSet = new Mock<DbSet<MethodModel>>();
            _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<MethodModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

            var options = new DbContextOptionsBuilder<SimuladorDbContext>()
                .UseInMemoryDatabase("MethodModelTestDb").Options;
            _mockContext = new Mock<SimuladorDbContext>(options);

            _mockContext.Setup(c => c.Set<MethodModel>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo = new MethodModelRepository(_mockContext.Object);
        }

        [TestMethod]
        public void Add_ShouldCallAddAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Add(_method);

            _mockSet.Verify(s => s.Add(_method), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrow_WhenMethodIsNull()
        {
            _repo.Add(null!);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllMethods()
        {
            var list = _repo.GetAll();

            CollectionAssert.AreEqual(_data.ToList(), list);
        }

        [TestMethod]
        public void Update_ShouldCallUpdateAndSaveChanges()
        {
            var updated = new MethodModel
            {
                Id = _method.Id,
                Name = "UpdatedMethod",
                ReturnType = "int",
                ClassId = _method.ClassId,
                IsAbstract = false,
                IsSealed = false,
                Accessibility = AccessibilityModifier.Private
            };
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Update(updated);

            _mockSet.Verify(s => s.Update(updated), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_ShouldThrow_WhenMethodIsNull()
        {
            _repo.Update(null!);
        }

        [TestMethod]
        public void Delete_ShouldCallRemoveAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Delete(_method);

            _mockSet.Verify(s => s.Remove(_method), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_ShouldThrow_WhenMethodIsNull()
        {
            _repo.Delete(null!);
        }

        [TestMethod]
        public void SaveChanges_ShouldCallContextSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.SaveChanges();

            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
