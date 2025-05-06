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
        private LocalVarModel _model = null!;
        private IQueryable<LocalVarModel> _data = null!;
        private Mock<DbSet<LocalVarModel>> _mockSet = null!;
        private Mock<SimuladorDbContext> _mockContext = null!;
        private LocalVarModelRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _model = new LocalVarModel
            {
                Id = Guid.NewGuid(),
                Name = "var1",
                Type = "int",
                MethodId = Guid.NewGuid()
            };
            _data = new List<LocalVarModel> { _model }.AsQueryable();
            _mockSet = new Mock<DbSet<LocalVarModel>>();
            _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<LocalVarModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());

            var options = new DbContextOptionsBuilder<SimuladorDbContext>()
                .UseInMemoryDatabase("LocalVarTestDb").Options;
            _mockContext = new Mock<SimuladorDbContext>(options);

            _mockContext.Setup(c => c.Set<LocalVarModel>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo = new LocalVarModelRepository(_mockContext.Object);
        }

        [TestMethod]
        public void Add_ShouldCallAddAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
            _repo.Add(_model);

            _mockSet.Verify(s => s.Add(_model), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrow_WhenModelIsNull()
        {
            _repo.Add(null!);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllItems()
        {
            var list = _repo.GetAll().ToList();
            CollectionAssert.AreEqual(_data.ToList(), list);
        }

        [TestMethod]
        public void Update_ShouldCallUpdateAndSaveChanges()
        {
            var updated = new LocalVarModel
            {
                Id = _model.Id,
                Name = "var2",
                Type = "string",
                MethodId = _model.MethodId
            };
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Update(updated);

            _mockSet.Verify(s => s.Update(updated), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_ShouldThrow_WhenModelIsNull()
        {
            _repo.Update(null!);
        }

        [TestMethod]
        public void Delete_ShouldCallRemoveAndSaveChanges()
        {
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo.Delete(_model);

            _mockSet.Verify(s => s.Remove(_model), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_ShouldThrow_WhenModelIsNull()
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
