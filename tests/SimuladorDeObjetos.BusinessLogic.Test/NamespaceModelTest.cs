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

    [TestMethod]
    public void GetFullName_ShouldReturnFullNamespacePath()
    {
        var root = new NamespaceModel { Id = Guid.NewGuid(), Name = "Root" };
        var child = new NamespaceModel { Id = Guid.NewGuid(), Name = "Child" };
        var grandchild = new NamespaceModel { Id = Guid.NewGuid(), Name = "Grandchild" };

        root.AddChild(child);
        child.AddChild(grandchild);

        var fullName = grandchild.GetFullName();

        Assert.AreEqual("Root.Child.Grandchild", fullName);
    }
}
