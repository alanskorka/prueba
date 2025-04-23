using Domain.Entities;
using Domain.Enums;

namespace Tests;

[TestClass]
    public class ClassModelTest
    {
        [TestMethod]
        public void ClassModel_PropertyAssignment_WorksCorrectly()
        {
            var id = Guid.NewGuid();
            var baseClassId = Guid.NewGuid();
            var name = "MyClass";
            var isAbstract = true;
            var isSealed = false;
            var attribute = new AttributeModel
            {
                Id = Guid.NewGuid(),
                Name = "attr1",
                Type = "int",
                ClassId = id,
                Accessibility = AccessibilityModifier.Public
            };
            var method = new MethodModel
            {
                Id = Guid.NewGuid(),
                Name = "Method1",
                ReturnType = "void",
                ClassId = id,
                IsAbstract = false,
                IsSealed = false,
                Accessibility = AccessibilityModifier.Private
            };
            var classModel = new ClassModel
            {
                Id = id,
                Name = name,
                IsAbstract = isAbstract,
                IsSealed = isSealed,
                BaseClassId = baseClassId,
                Attributes = new List<AttributeModel> { attribute },
                Methods = new List<MethodModel> { method }
            };
            Assert.AreEqual(id, classModel.Id);
            Assert.AreEqual(name, classModel.Name);
            Assert.AreEqual(isAbstract, classModel.IsAbstract);
            Assert.AreEqual(isSealed, classModel.IsSealed);
            Assert.AreEqual(baseClassId, classModel.BaseClassId);
            Assert.AreEqual(1, classModel.Attributes.Count);
            Assert.AreEqual(attribute, classModel.Attributes[0]);
            Assert.AreEqual(1, classModel.Methods.Count);
            Assert.AreEqual(method, classModel.Methods[0]);
        }

        [TestMethod]
        public void ClassModel_DefaultConstructor_InitializesCollections()
        {
            var classModel = new ClassModel();
            Assert.IsNotNull(classModel.Attributes);
            Assert.IsNotNull(classModel.Methods);
            Assert.AreEqual(0, classModel.Attributes.Count);
            Assert.AreEqual(0, classModel.Methods.Count);
        }
    }
