using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using SimuladorDeObjetos.Application.DTOs;

namespace SimuladorDeObjetos.Application.test;

[TestClass]
public class MappingProfileTest
{
    private IMapper _mapper = null!;

    [TestInitialize]
    public void Init()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        config.AssertConfigurationIsValid();
        _mapper = config.CreateMapper();
    }

    [TestMethod]
    public void AttributeDto_To_Model_And_Back()
    {
        var dto = new AttributeDto
        {
            Id = Guid.NewGuid(),
            Name = "Attr",
            Type = "string",
            ClassId = Guid.NewGuid(),
            Accessibility = AccessibilityModifier.Protected
        };

        var model = _mapper.Map<AttributeModel>(dto);
        var mappedBack = _mapper.Map<AttributeDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);
        Assert.AreEqual(dto.Type, mappedBack.Type);
        Assert.AreEqual(dto.ClassId, mappedBack.ClassId);
        Assert.AreEqual(dto.Accessibility, mappedBack.Accessibility);
    }

    [TestMethod]
    public void ParamDto_To_Model_And_Back()
    {
        var dto = new ParamDto { Id = Guid.NewGuid(), Name = "p1", Type = "int", MethodId = Guid.NewGuid() };
        var model = _mapper.Map<ParamModel>(dto);
        var mappedBack = _mapper.Map<ParamDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);
        Assert.AreEqual(dto.Type, mappedBack.Type);
        Assert.AreEqual(dto.MethodId, mappedBack.MethodId);
    }

    [TestMethod]
    public void LocalVarDto_To_Model_And_Back()
    {
        var dto = new LocalVarDto { Id = Guid.NewGuid(), Name = "v1", Type = "bool", MethodId = Guid.NewGuid() };
        var model = _mapper.Map<LocalVarModel>(dto);
        var mappedBack = _mapper.Map<LocalVarDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);
        Assert.AreEqual(dto.Type, mappedBack.Type);
        Assert.AreEqual(dto.MethodId, mappedBack.MethodId);
    }

    [TestMethod]
    public void MethodCallDto_To_Model_And_Back()
    {
        var dto = new MethodCallDto
        {
            MethodName = "Call", ReferenceType = ReferenceTypeInvocation.Attribute, ReferenceName = "obj"
        };
        var model = _mapper.Map<MethodCallModel>(dto);
        var mappedBack = _mapper.Map<MethodCallDto>(model);

        Assert.AreEqual(dto.MethodName, mappedBack.MethodName);
        Assert.AreEqual(dto.ReferenceType, mappedBack.ReferenceType);
        Assert.AreEqual(dto.ReferenceName, mappedBack.ReferenceName);
    }

    [TestMethod]
    public void MethodDto_To_Model_And_Back()
    {
        var dto = new MethodDto
        {
            Id = Guid.NewGuid(),
            Name = "DoSomething",
            ReturnType = "void",
            ClassId = Guid.NewGuid(),
            IsAbstract = false,
            IsSealed = false,
            Accessibility = AccessibilityModifier.Private,
            Params = new(),
            Vars = new(),
            MethodsCalled = new()
        };

        var model = _mapper.Map<MethodModel>(dto);
        var mappedBack = _mapper.Map<MethodDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);
        Assert.AreEqual(dto.ReturnType, mappedBack.ReturnType);
        Assert.AreEqual(dto.ClassId, mappedBack.ClassId);
        Assert.AreEqual(dto.IsAbstract, mappedBack.IsAbstract);
        Assert.AreEqual(dto.IsSealed, mappedBack.IsSealed);
        Assert.AreEqual(dto.Accessibility, mappedBack.Accessibility);
    }

    [TestMethod]
    public void ClassDto_To_Model_And_Back()
    {
        var classId = Guid.NewGuid();
        var dto = new ClassDto
        {
            Id = classId,
            Name = "TestClass",
            IsAbstract = false,
            IsSealed = true,
            BaseClassId = null,
            Attributes =
                new List<AttributeModel>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "attr1",
                        Type = "string",
                        ClassId = classId,
                        Accessibility = AccessibilityModifier.Public
                    }
                },
            Methods = new List<MethodModel>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "M1",
                    ReturnType = "void",
                    ClassId = classId,
                    Accessibility = AccessibilityModifier.Private
                }
            }
        };

        var model = _mapper.Map<ClassModel>(dto);
        var mappedBack = _mapper.Map<ClassDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);
        Assert.AreEqual(dto.IsAbstract, mappedBack.IsAbstract);
        Assert.AreEqual(dto.IsSealed, mappedBack.IsSealed);
        Assert.AreEqual(dto.Attributes.Count, mappedBack.Attributes.Count);
        Assert.AreEqual(dto.Methods.Count, mappedBack.Methods.Count);
    }

    [TestMethod]
    public void InterfaceMethodDto_To_Model_And_Back()
    {
        var dto = new InterfaceMethodDto
        {
            Id = 1,
            Name = "DoSomething",
            ReturnType = "string",
            Parameters = new List<ParamDto>
            {
                new() { Id = Guid.NewGuid(), Name = "x", Type = "int", MethodId = Guid.NewGuid() }
            }
        };

        var model = _mapper.Map<InterfaceMethodModel>(dto);
        var mappedBack = _mapper.Map<InterfaceMethodDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);
        Assert.AreEqual(dto.ReturnType, mappedBack.ReturnType);
        Assert.AreEqual(dto.Parameters.Count, mappedBack.Parameters.Count);
        Assert.AreEqual(dto.Parameters[0].Name, mappedBack.Parameters[0].Name);
        Assert.AreEqual(dto.Parameters[0].Type, mappedBack.Parameters[0].Type);
    }

    [TestMethod]
    public void InterfaceDto_To_Model_And_Back()
    {
        var dto = new InterfaceDto
        {
            Id = 1,
            Name = "IMyInterface",
            Methods = new List<InterfaceMethodDto>
            {
                new InterfaceMethodDto
                {
                    Id = 10,
                    Name = "DoSomething",
                    ReturnType = "void",
                    Parameters = new List<ParamDto>
                    {
                        new ParamDto
                        {
                            Id = Guid.NewGuid(),
                            Name = "x",
                            Type = "int",
                            MethodId = Guid.NewGuid()
                        }
                    }
                }
            }
        };

        var model = _mapper.Map<InterfaceModel>(dto);

        var mappedBack = _mapper.Map<InterfaceDto>(model);

        Assert.AreEqual(dto.Id, mappedBack.Id);
        Assert.AreEqual(dto.Name, mappedBack.Name);

        Assert.AreEqual(1, mappedBack.Methods.Count);
        Assert.AreEqual("DoSomething", mappedBack.Methods[0].Name);

        Assert.AreEqual(1, mappedBack.Methods[0].Parameters.Count);
        Assert.AreEqual("x", mappedBack.Methods[0].Parameters[0].Name);
        Assert.AreEqual("int", mappedBack.Methods[0].Parameters[0].Type);
    }
}
