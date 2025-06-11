using AutoMapper;
using Domain.Entities;

namespace SimuladorDeObjetos.Application.DTOs;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AttributeDto, AttributeModel>()
            .ForMember(dest => dest.ConcreteTypeId, opt => opt.MapFrom(src => src.ConcreteTypeId))
            .ForMember(dest => dest.ConcreteType, opt => opt.MapFrom(src => src.ConcreteType))
            .ReverseMap();

        CreateMap<ParamDto, ParamModel>()
            .ForMember(dest => dest.ConcreteTypeId, opt => opt.MapFrom(src => src.ConcreteTypeId))
            .ForMember(dest => dest.ConcreteType, opt => opt.MapFrom(src => src.ConcreteType))
            .ReverseMap();

        CreateMap<LocalVarDto, LocalVarModel>()
            .ForMember(dest => dest.ConcreteTypeId, opt => opt.MapFrom(src => src.ConcreteTypeId))
            .ForMember(dest => dest.ConcreteType, opt => opt.MapFrom(src => src.ConcreteType))
            .ReverseMap();

        CreateMap<MethodCallDto, MethodCallModel>()
            .ForMember(dest => dest.ConcreteParameterTypes, opt => opt.MapFrom(src => src.ConcreteParameterTypes))
            .ForMember(dest => dest.ConcreteParameters, opt => opt.MapFrom(src => src.ConcreteParameters))
            .ReverseMap();

        CreateMap<MethodDto, MethodModel>().ReverseMap();

        CreateMap<InterfaceDto, InterfaceModel>().ReverseMap();
        CreateMap<InterfaceMethodDto, InterfaceMethodModel>().ReverseMap();

        var mappingExpression = CreateMap<NamespaceDto, NamespaceModel>()
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
            .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
            .ReverseMap();

        mappingExpression.ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
        mappingExpression.ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent!));

        CreateMap<ClassDto, ClassModel>()
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
            .ForMember(dest => dest.Methods, opt => opt.MapFrom(src => src.Methods))
            .ForMember(dest => dest.ImplementedInterfaces, opt => opt.MapFrom(src => src.ImplementedInterfaces))
            .ReverseMap()
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
            .ForMember(dest => dest.Methods, opt => opt.MapFrom(src => src.Methods))
            .ForMember(dest => dest.ImplementedInterfaces, opt => opt.MapFrom(src => src.ImplementedInterfaces));
    }
}
