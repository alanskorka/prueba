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
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.Infrastructure.Test;

    [TestClass]
    public class AtributteModelRepositoryTest
    {
        private AttributeModel _attribute = null!;
        private IQueryable<AttributeModel> _data = null!;
        private Mock<DbSet<AttributeModel>> _mockSet = null!;
        private Mock<SimuladorDbContext> _mockContext = null!;
        private AtributteModelRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _attribute = new AttributeModel
            {
                Id = Guid.NewGuid(),
                Name = "TestAttribute",
                Type = "string",
                ClassId = Guid.NewGuid(),
                Accessibility = AccessibilityModifier.Public
            };

            _data = new List<AttributeModel> { _attribute }.AsQueryable();

            _mockSet = new Mock<DbSet<AttributeModel>>();
            _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<AttributeModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

            var options = new DbContextOptionsBuilder<SimuladorDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            _mockContext = new Mock<SimuladorDbContext>(options);

            _mockContext
                .Setup(c => c.Set<AttributeModel>())
                .Returns(_mockSet.Object);

            _repo = new AtributteModelRepository(_mockContext.Object);
        }

        [TestMethod]
        public void Add_ShouldCallAddAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Add(_attribute);

            _mockSet.Verify(s => s.Add(_attribute), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrow_WhenAttributeIsNull()
        {
            _repo.Add(null!);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllAttributes()
        {
            var result = _repo.GetAll();
            CollectionAssert.AreEqual(_data.ToList(), result);
        }

        [TestMethod]
        public void Update_ShouldCallUpdateAndSaveChanges()
        {
            var updated = new AttributeModel
            {
                Id = _attribute.Id,
                Name = "Updated",
                Type = "int",
                ClassId = _attribute.ClassId,
                Accessibility = AccessibilityModifier.Private
            };
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Update(updated);

            _mockSet.Verify(s => s.Update(updated), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_ShouldThrow_WhenAttributeIsNull()
        {
            _repo.Update(null!);
        }

        [TestMethod]
        public void Delete_ShouldCallRemoveAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Delete(_attribute);

            _mockSet.Verify(s => s.Remove(_attribute), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_ShouldThrow_WhenAttributeIsNull()
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
