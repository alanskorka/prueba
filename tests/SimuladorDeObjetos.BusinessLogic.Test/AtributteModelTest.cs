using Domain.Entities;
using Domain.Enums;

namespace Tests;

[TestClass]
public class AttributeModelTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeProperties()
    {
        Guid id = Guid.NewGuid();
        Guid classId = Guid.NewGuid();
        AttributeModel attribute = new AttributeModel
        {
            Id = id,
            Name = "miAtributo",
            Type = "string",
            ClassId = classId,
            Accessibility = AccessibilityModifier.Private
        };
        Assert.AreEqual(id, attribute.Id);
        Assert.AreEqual("miAtributo", attribute.Name);
        Assert.AreEqual("string", attribute.Type);
        Assert.AreEqual(classId, attribute.ClassId);
        Assert.AreEqual(AccessibilityModifier.Private, attribute.Accessibility);
    }

    [TestMethod]
    public void AttributeModel_StaticFlag_AssignedCorrectly()
    {
        var attr = new AttributeModel { IsStatic = true };
        Assert.IsTrue(attr.IsStatic);

        attr.IsStatic = false;
        Assert.IsFalse(attr.IsStatic);
    }
}
