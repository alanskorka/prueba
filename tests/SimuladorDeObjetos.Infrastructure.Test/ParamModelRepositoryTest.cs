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
        private ParamModel _param = null!;
        private IQueryable<ParamModel> _data = null!;
        private Mock<DbSet<ParamModel>> _mockSet = null!;
        private Mock<SimuladorDbContext> _mockContext = null!;
        private ParamModelRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _param = new ParamModel
            {
                Id = Guid.NewGuid(),
                Name = "p1",
                Type = "int",
                MethodId = Guid.NewGuid()
            };
            _data = new List<ParamModel> { _param }.AsQueryable();
            _mockSet = new Mock<DbSet<ParamModel>>();
            _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<ParamModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());
            var options = new DbContextOptionsBuilder<SimuladorDbContext>()
                .UseInMemoryDatabase("ParamModelTestDb").Options;
            _mockContext = new Mock<SimuladorDbContext>(options);
            _mockContext.Setup(c => c.Set<ParamModel>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
            _repo = new ParamModelRepository(_mockContext.Object);
        }

        [TestMethod]
        public void Add_ShouldCallAddAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
            _repo.Add(_param);
            _mockSet.Verify(s => s.Add(_param), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrow_WhenParamIsNull()
        {
            _repo.Add(null!);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllParams()
        {
            var list = _repo.GetAll();
            CollectionAssert.AreEqual(_data.ToList(), list);
        }

        [TestMethod]
        public void Update_ShouldCallUpdateAndSaveChanges()
        {
            var updated = new ParamModel
            {
                Id = _param.Id,
                Name = "p2",
                Type = "string",
                MethodId = _param.MethodId
            };
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
            _repo.Update(updated);
            _mockSet.Verify(s => s.Update(updated), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_ShouldThrow_WhenParamIsNull()
        {
            _repo.Update(null!);
        }

        [TestMethod]
        public void Delete_ShouldCallRemoveAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
            _repo.Delete(_param);
            _mockSet.Verify(s => s.Remove(_param), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_ShouldThrow_WhenParamIsNull()
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
