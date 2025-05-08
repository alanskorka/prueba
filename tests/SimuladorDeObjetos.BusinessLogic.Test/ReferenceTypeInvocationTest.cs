using Domain.Enums;

namespace Tests;

 [TestClass]
    public class ReferenceTypeInvocationTest
    {
        [TestMethod]
        public void ReferenceTypeInvocation_This_ReturnsCorrectEnumValue()
        {
            var reference = ReferenceTypeInvocation.This;
            Assert.AreEqual(ReferenceTypeInvocation.This, reference);
        }

        [TestMethod]
        public void ReferenceTypeInvocation_Attribute_ReturnsCorrectEnumValue()
        {
            var reference = ReferenceTypeInvocation.Attribute;
            Assert.AreEqual(ReferenceTypeInvocation.Attribute, reference);
        }

        [TestMethod]
        public void ReferenceTypeInvocation_Parameter_ReturnsCorrectEnumValue()
        {
            var reference = ReferenceTypeInvocation.Parameter;
            Assert.AreEqual(ReferenceTypeInvocation.Parameter, reference);
        }

        [TestMethod]
        public void ReferenceTypeInvocation_LocalVar_ReturnsCorrectEnumValue()
        {
            var reference = ReferenceTypeInvocation.LocalVar;
            Assert.AreEqual(ReferenceTypeInvocation.LocalVar, reference);
        }

        [TestMethod]
        public void ReferenceTypeInvocation_Base_ReturnsCorrectEnumValue()
        {
            var reference = ReferenceTypeInvocation.Base;
            Assert.AreEqual(ReferenceTypeInvocation.Base, reference);
        }

        [TestMethod]
        public void ReferenceTypeInvocation_EnumValuesAreCorrect()
        {
            Assert.AreEqual(0, (int)ReferenceTypeInvocation.This);
            Assert.AreEqual(1, (int)ReferenceTypeInvocation.Attribute);
            Assert.AreEqual(2, (int)ReferenceTypeInvocation.Parameter);
            Assert.AreEqual(3, (int)ReferenceTypeInvocation.LocalVar);
            Assert.AreEqual(4, (int)ReferenceTypeInvocation.Base);
        }

        [TestMethod]
        public void ReferenceTypeInvocation_EnumValues_AreDistinct()
        {
            Assert.AreNotEqual(ReferenceTypeInvocation.This, ReferenceTypeInvocation.Attribute);
            Assert.AreNotEqual(ReferenceTypeInvocation.This, ReferenceTypeInvocation.Parameter);
            Assert.AreNotEqual(ReferenceTypeInvocation.This, ReferenceTypeInvocation.LocalVar);
            Assert.AreNotEqual(ReferenceTypeInvocation.This, ReferenceTypeInvocation.Base);
            Assert.AreNotEqual(ReferenceTypeInvocation.Attribute, ReferenceTypeInvocation.Parameter);
            Assert.AreNotEqual(ReferenceTypeInvocation.Attribute, ReferenceTypeInvocation.LocalVar);
            Assert.AreNotEqual(ReferenceTypeInvocation.Attribute, ReferenceTypeInvocation.Base);
            Assert.AreNotEqual(ReferenceTypeInvocation.Parameter, ReferenceTypeInvocation.LocalVar);
            Assert.AreNotEqual(ReferenceTypeInvocation.Parameter, ReferenceTypeInvocation.Base);
            Assert.AreNotEqual(ReferenceTypeInvocation.LocalVar, ReferenceTypeInvocation.Base);
        }
    }
