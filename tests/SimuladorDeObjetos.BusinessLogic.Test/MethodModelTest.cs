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
            var isVirtual = true;
            var isOverride = false;
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
                IsVirtual = isVirtual,
                IsOverride = isOverride,
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
            Assert.AreEqual(isVirtual, method.IsVirtual);
            Assert.AreEqual(isOverride, method.IsOverride);
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
            Assert.IsFalse(method.IsVirtual);
            Assert.IsFalse(method.IsOverride);
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

        [TestMethod]
        public void MethodModel_StaticFlag_AssignedCorrectly()
        {
            var method = new MethodModel { IsStatic = true };
            Assert.IsTrue(method.IsStatic);

            method.IsStatic = false;
            Assert.IsFalse(method.IsStatic);
        }

        [TestMethod]
        public void MethodModel_VirtualFlag_AssignedCorrectly()
        {
            var method = new MethodModel { IsVirtual = true };
            Assert.IsTrue(method.IsVirtual);

            method.IsVirtual = false;
            Assert.IsFalse(method.IsVirtual);
        }

        [TestMethod]
        public void MethodModel_OverrideFlag_AssignedCorrectly()
        {
            var method = new MethodModel { IsOverride = true };
            Assert.IsTrue(method.IsOverride);

            method.IsOverride = false;
            Assert.IsFalse(method.IsOverride);
        }

        [TestMethod]
        public void MethodModel_Should_Have_IsStatic_Property()
        {
            var method = new MethodModel();
            method.IsStatic = true;

            Assert.IsTrue(method.IsStatic);
        }

        [TestMethod]
        public void ParamModel_ConcreteType_AssignedCorrectly()
        {
            var concreteTypeId = Guid.NewGuid();
            var concreteType = new ClassModel { Id = concreteTypeId, Name = "ConcreteClass" };

            var param = new ParamModel
            {
                Id = Guid.NewGuid(),
                Name = "param",
                Type = "BaseClass",
                ConcreteTypeId = concreteTypeId,
                ConcreteType = concreteType
            };

            Assert.AreEqual(concreteTypeId, param.ConcreteTypeId);
            Assert.AreEqual(concreteType, param.ConcreteType);
        }

        [TestMethod]
        public void LocalVarModel_ConcreteType_AssignedCorrectly()
        {
            var concreteTypeId = Guid.NewGuid();
            var concreteType = new ClassModel { Id = concreteTypeId, Name = "ConcreteClass" };

            var localVar = new LocalVarModel
            {
                Id = Guid.NewGuid(),
                Name = "localVar",
                Type = "BaseClass",
                ConcreteTypeId = concreteTypeId,
                ConcreteType = concreteType
            };

            Assert.AreEqual(concreteTypeId, localVar.ConcreteTypeId);
            Assert.AreEqual(concreteType, localVar.ConcreteType);
        }

        [TestMethod]
        public void MethodCallModel_ConcreteParameters_AssignedCorrectly()
        {
            var concreteType1 = new ClassModel { Id = Guid.NewGuid(), Name = "ConcreteClass1" };
            var concreteType2 = new ClassModel { Id = Guid.NewGuid(), Name = "ConcreteClass2" };

            var methodCall = new MethodCallModel
            {
                Id = Guid.NewGuid(),
                MethodName = "TestMethod",
                ConcreteParameterTypes = new List<Guid> { concreteType1.Id, concreteType2.Id },
                ConcreteParameters = new List<ClassModel> { concreteType1, concreteType2 }
            };

            Assert.AreEqual(2, methodCall.ConcreteParameterTypes.Count);
            Assert.AreEqual(2, methodCall.ConcreteParameters.Count);
            Assert.AreEqual(concreteType1.Id, methodCall.ConcreteParameterTypes[0]);
            Assert.AreEqual(concreteType2.Id, methodCall.ConcreteParameterTypes[1]);
            Assert.AreEqual(concreteType1, methodCall.ConcreteParameters[0]);
            Assert.AreEqual(concreteType2, methodCall.ConcreteParameters[1]);
        }
    }
