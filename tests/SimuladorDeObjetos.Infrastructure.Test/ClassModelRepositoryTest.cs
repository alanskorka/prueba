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
    public class ClassModelRepositoryTest
    {
        private ClassModel _model = null!;
        private IQueryable<ClassModel> _data = null!;
        private Mock<DbSet<ClassModel>> _mockSet = null!;
        private Mock<SimuladorDbContext> _mockContext = null!;
        private ClassModelRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _model = new ClassModel { Id = Guid.NewGuid(), Name = "TestClass" };

            _data = new List<ClassModel> { _model }.AsQueryable();

            _mockSet = new Mock<DbSet<ClassModel>>();
            _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<ClassModel>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());
            _mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids =>
                _data.SingleOrDefault(e => e.Id == (Guid)ids[0]));

            var options = new DbContextOptionsBuilder<SimuladorDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            _mockContext = new Mock<SimuladorDbContext>(options);

            _mockContext.Setup(c => c.Set<ClassModel>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.Add(It.IsAny<ClassModel>())).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _repo = new ClassModelRepository(_mockContext.Object);
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
        public void GetAll_ShouldReturnAllEntities()
        {
            var result = _repo.GetAll().ToList();
            CollectionAssert.AreEqual(_data.ToList(), result);
        }

        [TestMethod]
        public void Update_ShouldCallUpdateAndSaveChanges()
        {
            var updated = new ClassModel { Id = _model.Id, Name = "Updated" };
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

        [TestMethod]
        public void GetById_ShouldReturnEntity_WhenExists()
        {
            var result = _repo.GetById(_model.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(_model.Name, result!.Name);
        }

        [TestMethod]
        public void GetById_ShouldReturnNull_WhenNotExists()
        {
            var result = _repo.GetById(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
