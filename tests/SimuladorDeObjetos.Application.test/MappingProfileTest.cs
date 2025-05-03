using AutoMapper;
using Domain.Entities;
using SimuladorDeObjetos.Application.DTOs;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MappingProfileTest
{
    private IMapper? _mapper;

    [TestInitialize]
    public void Init()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        config.AssertConfigurationIsValid();
        _mapper = config.CreateMapper();
    }

    [TestMethod]
        public void AttributeDto_To_AttributeModel_And_Back()
        {
            var dto = new AttributeDto
            {
                Id = Guid.NewGuid(),
                Name = "AttrName",
                Type = "string",
                ClassId = Guid.NewGuid(),
                Accessibility = Domain.Enums.AccessibilityModifier.Private
            };

            // Map to model
            var model = _mapper.Map<AttributeModel>(dto);
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.Type, model.Type);
            Assert.AreEqual(dto.ClassId, model.ClassId);
            Assert.AreEqual(dto.Accessibility, model.Accessibility);

            // Map back to dto
            var dto2 = _mapper.Map<AttributeDto>(model);
            Assert.AreEqual(dto.Id, dto2.Id);
            Assert.AreEqual(dto.Name, dto2.Name);
            Assert.AreEqual(dto.Type, dto2.Type);
            Assert.AreEqual(dto.ClassId, dto2.ClassId);
            Assert.AreEqual(dto.Accessibility, dto2.Accessibility);
        }

        [TestMethod]
        public void ParamDto_To_ParamModel_And_Back()
        {
            var dto = new ParamDto
            {
                Id = Guid.NewGuid(),
                Name = "param1",
                Type = "int",
                MethodId = Guid.NewGuid()
            };

            var model = _mapper.Map<ParamModel>(dto);
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.Type, model.Type);
            Assert.AreEqual(dto.MethodId, model.MethodId);

            var dto2 = _mapper.Map<ParamDto>(model);
            Assert.AreEqual(dto.Id, dto2.Id);
            Assert.AreEqual(dto.Name, dto2.Name);
            Assert.AreEqual(dto.Type, dto2.Type);
            Assert.AreEqual(dto.MethodId, dto2.MethodId);
        }

        [TestMethod]
        public void LocalVarDto_To_LocalVarModel_And_Back()
        {
            var dto = new LocalVarDto
            {
                Id = Guid.NewGuid(),
                Name = "var1",
                Type = "bool",
                MethodId = Guid.NewGuid()
            };

            var model = _mapper.Map<LocalVarModel>(dto);
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.Type, model.Type);
            Assert.AreEqual(dto.MethodId, model.MethodId);

            var dto2 = _mapper.Map<LocalVarDto>(model);
            Assert.AreEqual(dto.Id, dto2.Id);
            Assert.AreEqual(dto.Name, dto2.Name);
            Assert.AreEqual(dto.Type, dto2.Type);
            Assert.AreEqual(dto.MethodId, dto2.MethodId);
        }

        [TestMethod]
        public void MethodCallDto_To_MethodCallModel_And_Back()
        {
            var dto = new MethodCallDto
            {
                MethodName = "CallMe",
                ReferenceType = Domain.Enums.ReferenceTypeInvocation.Base
            };

            var model = _mapper.Map<MethodCallModel>(dto);
            Assert.AreEqual(dto.MethodName, model.MethodName);
            Assert.AreEqual(dto.ReferenceType, model.ReferenceType);

            var dto2 = _mapper.Map<MethodCallDto>(model);
            Assert.AreEqual(dto.MethodName, dto2.MethodName);
            Assert.AreEqual(dto.ReferenceType, dto2.ReferenceType);
        }

        [TestMethod]
        public void MethodDto_To_MethodModel_And_Back()
        {
            var dto = new MethodDto
            {
                Id = Guid.NewGuid(),
                Name = "Mtd",
                ReturnType = "void",
                ClassId = Guid.NewGuid(),
                IsAbstract = true,
                IsSealed = false,
                Accessibility = Domain.Enums.AccessibilityModifier.Public
            };
            var model = _mapper.Map<MethodModel>(dto);
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.ReturnType, model.ReturnType);
            Assert.AreEqual(dto.ClassId, model.ClassId);
            Assert.AreEqual(dto.IsAbstract, model.IsAbstract);
            Assert.AreEqual(dto.IsSealed, model.IsSealed);
            Assert.AreEqual(dto.Accessibility, model.Accessibility);
            Assert.IsNotNull(model.Params);
            Assert.IsNotNull(model.Vars);
            Assert.IsNotNull(model.MethodsCalled);

            var dto2 = _mapper.Map<MethodDto>(model);
            Assert.AreEqual(dto.Id, dto2.Id);
            Assert.AreEqual(dto.Name, dto2.Name);
        }

        [TestMethod]
        public void ClassDto_To_ClassModel_And_Back()
        {
            var dto = new ClassDto
            {
                Id = Guid.NewGuid(),
                Name = "MyClass",
                IsAbstract = false,
                BaseClassId = null,
                IsSealed = true
            };
            dto.Attributes.Add(new AttributeDto { Id = Guid.NewGuid(), Name = "A", Type = "int", ClassId = dto.Id, Accessibility = Domain.Enums.AccessibilityModifier.Public });
            dto.Methods.Add(new MethodDto { Id = Guid.NewGuid(), Name = "M", ReturnType = "void", ClassId = dto.Id, Accessibility = Domain.Enums.AccessibilityModifier.Public });

            var model = _mapper.Map<ClassModel>(dto);
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.IsAbstract, model.IsAbstract);
            Assert.AreEqual(dto.IsSealed, model.IsSealed);
            Assert.AreEqual(dto.Attributes.Count, model.Attributes.Count);
            Assert.AreEqual(dto.Methods.Count, model.Methods.Count);

            var dto2 = _mapper.Map<ClassDto>(model);
            Assert.AreEqual(dto.Id, dto2.Id);
            Assert.AreEqual(dto.Name, dto2.Name);
        }
}
