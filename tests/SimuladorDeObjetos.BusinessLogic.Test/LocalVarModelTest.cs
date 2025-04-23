using Domain.Entities;

namespace Tests;

[TestClass]
public class LocalVarModelTest
{
    [TestMethod]
    public void LocalVarModel_PropertyAssignment_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var methodId = Guid.NewGuid();
        var name = "variable1";
        var type = "int";
        var localVar = new LocalVarModel
        {
            Id = id,
            Name = name,
            Type = type,
            MethodId = methodId
        };
        Assert.AreEqual(id, localVar.Id);
        Assert.AreEqual(name, localVar.Name);
        Assert.AreEqual(type, localVar.Type);
        Assert.AreEqual(methodId, localVar.MethodId);
    }

    [TestMethod]
    public void LocalVarModel_DefaultConstructor_InitializesProperties()
    {
        var localVar = new LocalVarModel();
        Assert.AreEqual(default(Guid), localVar.Id);
        Assert.IsNull(localVar.Name);
        Assert.IsNull(localVar.Type);
        Assert.AreEqual(default(Guid), localVar.MethodId);
    }
}
