using Domain.Entities;
using Domain.Enums;

namespace Tests;

 [TestClass]
    public class MethodModelTest
    {
        [TestMethod]
        public void MethodModel_PropertyAssignment_WorksCorrectly()
        {
            var id = Guid.NewGuid();
            var classId = Guid.NewGuid();
            var name = "MyMethod";
            var returnType = "void";
            var isAbstract = true;
            var isSealed = false;
            var accessibility = AccessibilityModifier.Public;

            var param = new ParamModel { Id = Guid.NewGuid(), Name = "param", Type = "int", MethodId = id };
            var localVar = new LocalVarModel { Id = Guid.NewGuid(), Name = "var", Type = "string", MethodId = id };
            var methodCall = new MethodCallModel { MethodName = "AnotherMethod", ReferenceType = ReferenceTypeInvocation.Parameter };
            var method = new MethodModel
            {
                Id = id,
                ClassId = classId,
                Name = name,
                ReturnType = returnType,
                IsAbstract = isAbstract,
                IsSealed = isSealed,
                Accessibility = accessibility,
                Params = new List<ParamModel> { param },
                Vars = new List<LocalVarModel> { localVar },
                MethodsCalled = new List<MethodCallModel> { methodCall }
            };
            Assert.AreEqual(id, method.Id);
            Assert.AreEqual(classId, method.ClassId);
            Assert.AreEqual(name, method.Name);
            Assert.AreEqual(returnType, method.ReturnType);
            Assert.AreEqual(isAbstract, method.IsAbstract);
            Assert.AreEqual(isSealed, method.IsSealed);
            Assert.AreEqual(accessibility, method.Accessibility);
            Assert.AreEqual(1, method.Params.Count);
            Assert.AreEqual(1, method.Vars.Count);
            Assert.AreEqual(1, method.MethodsCalled.Count);
        }

        [TestMethod]
        public void MethodModel_DefaultConstructor_InitializesCollections()
        {
            var method = new MethodModel();
            Assert.AreEqual(default(Guid), method.Id);
            Assert.IsNull(method.Name);
            Assert.IsNull(method.ReturnType);
            Assert.AreEqual(default(Guid), method.ClassId);
            Assert.IsFalse(method.IsAbstract);
            Assert.IsFalse(method.IsSealed);
            Assert.AreEqual(default(AccessibilityModifier), method.Accessibility);
            Assert.IsNotNull(method.Params);
            Assert.IsNotNull(method.Vars);
            Assert.IsNotNull(method.MethodsCalled);
            Assert.AreEqual(0, method.Params.Count);
            Assert.AreEqual(0, method.Vars.Count);
            Assert.AreEqual(0, method.MethodsCalled.Count);
        }

        [TestMethod]
        public void MethodModel_AssignsAllAccessibilityModifierValues()
        {
            foreach (AccessibilityModifier modifier in Enum.GetValues(typeof(AccessibilityModifier)))
            {
                var method = new MethodModel
                {
                    Accessibility = modifier
                };
                Assert.AreEqual(modifier, method.Accessibility);
            }
        }
    }
