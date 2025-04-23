using Domain.Entities;

namespace Tests;

[TestClass]
public class MethodCallModelTest
{
    [TestMethod]
    public void MethodCallModel_PropertyAssignment_WorksCorrectly()
    {
        var methodName = "MyMethod";
        var referenceType = ReferenceTypeInvocation.Attribute;
        var methodCall = new MethodCallModel
        {
            MethodName = methodName,
            ReferenceType = referenceType
        };
        Assert.AreEqual(methodName, methodCall.MethodName);
        Assert.AreEqual(referenceType, methodCall.ReferenceType);
    }

    [TestMethod]
    public void MethodCallModel_DefaultConstructor_InitializesProperties()
    {
        var methodCall = new MethodCallModel();
        Assert.IsNull(methodCall.MethodName);
        Assert.AreEqual(ReferenceTypeInvocation.This, methodCall.ReferenceType);
    }
}
