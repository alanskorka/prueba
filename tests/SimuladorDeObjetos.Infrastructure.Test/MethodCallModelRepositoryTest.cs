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
        private MethodCallModel _call = null!;
        private IQueryable<MethodCallModel> _data = null!;
        private Mock<DbSet<MethodCallModel>> _mockSet = null!;
        private Mock<SimuladorDbContext> _mockContext = null!;
        private MethodCallModelRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _call = new MethodCallModel
            {
                MethodName    = "CallA",
                ReferenceType = ReferenceTypeInvocation.This
            };

            _data = new List<MethodCallModel> { _call }.AsQueryable();

            _mockSet = new Mock<DbSet<MethodCallModel>>();
            _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<MethodCallModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

            var options = new DbContextOptionsBuilder<SimuladorDbContext>()
                .UseInMemoryDatabase("MethodCallTestDb").Options;
            _mockContext = new Mock<SimuladorDbContext>(options);

            _mockContext.Setup(c => c.Set<MethodCallModel>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo = new MethodCallModelRepository(_mockContext.Object);
        }

        [TestMethod]
        public void Add_ShouldCallAddAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
            _repo.Add(_call);

            _mockSet.Verify(s => s.Add(_call), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrow_WhenCallIsNull()
        {
            _repo.Add(null!);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllCalls()
        {
            var list = _repo.GetAll();

            CollectionAssert.AreEqual(_data.ToList(), list);
        }

        [TestMethod]
        public void Update_ShouldCallUpdateAndSaveChanges()
        {
            var updated = new MethodCallModel
            {
                MethodName    = "CallB",
                ReferenceType = ReferenceTypeInvocation.Base
            };
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Update(updated);

            _mockSet.Verify(s => s.Update(updated), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_ShouldThrow_WhenCallIsNull()
        {
            _repo.Update(null!);
        }

        [TestMethod]
        public void Delete_ShouldCallRemoveAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Delete(_call);

            _mockSet.Verify(s => s.Remove(_call), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_ShouldThrow_WhenCallIsNull()
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
