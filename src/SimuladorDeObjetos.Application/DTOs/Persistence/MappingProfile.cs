using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SimuladorDeObjetos.Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AttributeDto, AttributeModel>().ReverseMap();
        CreateMap<ParamDto, ParamModel>().ReverseMap();
        CreateMap<LocalVarDto, LocalVarModel>().ReverseMap();
        CreateMap<MethodCallDto, MethodCallModel>().ReverseMap();
        CreateMap<MethodDto, MethodModel>().ReverseMap();

        CreateMap<ClassDto, ClassModel>()
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
            .ForMember(dest => dest.Methods, opt => opt.MapFrom(src => src.Methods))
            .ReverseMap()
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
            .ForMember(dest => dest.Methods, opt => opt.MapFrom(src => src.Methods));
    }
}
