using Domain.Enums;

namespace Tests;

[TestClass]
public class AccessibilityModifierTest
{
    [TestMethod]
    public void AccessibilityModifier_Public_ReturnsTrueForPublic()
    {
        var modifier = AccessibilityModifier.Public;
        Assert.AreEqual(AccessibilityModifier.Public, modifier);
    }

    [TestMethod]
    public void AccessibilityModifier_Protected_ReturnsTrueForProtected()
    {
        var modifier = AccessibilityModifier.Protected;
        Assert.AreEqual(AccessibilityModifier.Protected, modifier);
    }

    [TestMethod]
    public void AccessibilityModifier_Private_ReturnsTrueForPrivate()
    {
        var modifier = AccessibilityModifier.Private;
        Assert.AreEqual(AccessibilityModifier.Private, modifier);
    }

    [TestMethod]
    public void AccessibilityModifier_EnumValuesAreCorrect()
    {
        Assert.AreEqual(0, (int)AccessibilityModifier.Public);
        Assert.AreEqual(1, (int)AccessibilityModifier.Protected);
        Assert.AreEqual(2, (int)AccessibilityModifier.Private);
    }

    [TestMethod]
    public void AccessibilityModifier_EnumValues_AreDistinct()
    {
        Assert.AreNotEqual(AccessibilityModifier.Public, AccessibilityModifier.Protected);
        Assert.AreNotEqual(AccessibilityModifier.Public, AccessibilityModifier.Private);
        Assert.AreNotEqual(AccessibilityModifier.Protected, AccessibilityModifier.Private);
    }
}
