using Domain.Entities;

namespace Tests;

[TestClass]
public class ParamModelTest
{
    [TestMethod]
    public void ParamModel_PropertyAssignment_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var methodId = Guid.NewGuid();
        var name = "param1";
        var type = "string";
        var param = new ParamModel
        {
            Id = id,
            Name = name,
            Type = type,
            MethodId = methodId
        };
        Assert.AreEqual(id, param.Id);
        Assert.AreEqual(name, param.Name);
        Assert.AreEqual(type, param.Type);
        Assert.AreEqual(methodId, param.MethodId);
    }

    [TestMethod]
    public void ParamModel_DefaultConstructor_InitializesProperties()
    {
        var param = new ParamModel();
        Assert.AreEqual(default(Guid), param.Id);
        Assert.IsNull(param.Name);
        Assert.IsNull(param.Type);
        Assert.AreEqual(default(Guid), param.MethodId);
    }
}
