using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimuladorDeObjetos.Infrastructure.Repositories;

namespace SimuladorDeObjetos.Infrastructure.Test;

    [TestClass]
    public class ClassModelRepositoryTest
    {
        [TestMethod]
        public void Add_ShouldStoreClassModel()
        {
            var data = new List<ClassModel>
            {
                new ClassModel { Name = "Test1" },
                new ClassModel { Name = "Test2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ClassModel>>();
            mockSet.As<IQueryable<ClassModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ClassModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ClassModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ClassModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<ClassModel>()).Returns(mockSet.Object);

            var repository = new ClassModelRepository(mockContext.Object);

            var result = repository.GetAll().ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test1", result[0].Name);
        }
    }
