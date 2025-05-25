using Domain.Entities;
using Domain.Enums;

namespace Tests;

[TestClass]
public class NamespaceModelTest
{
    [TestMethod]
    public void AddChildNamespace_ShouldAddNamespaceToChildrenList()
    {
        var parent = new NamespaceModel { Id = Guid.NewGuid(), Name = "Parent" };
        var child = new NamespaceModel { Id = Guid.NewGuid(), Name = "Child" };

        parent.AddChild(child);

        Assert.AreEqual(1, parent.Children.Count);
        Assert.AreEqual("Child", parent.Children[0].Name);
    }

    [TestMethod]
    public void AddChildNamespace_ShouldSetParentReference()
    {
        var parent = new NamespaceModel { Id = Guid.NewGuid(), Name = "Parent" };
        var child = new NamespaceModel { Id = Guid.NewGuid(), Name = "Child" };

        parent.AddChild(child);

        Assert.AreEqual(parent, child.Parent);
    }
}
