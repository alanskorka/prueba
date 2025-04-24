namespace SimuladorDeObjetos.Infrastructure.Test;

    [TestClass]
    public class ClassModelRepositoryTest
    {
        [TestMethod]
        public void Add_ShouldStoreClassModel()
        {
            var repository = new ClassModelRepository();
            var classModel = new ClassModel { Name = "TestClass" };
            repository.Add(classModel);
            var all = repository.GetAll();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual("TestClass", all[0].Name);
        }
    }
}
