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
        CreateMap<ClassDto, ClassModel>().ReverseMap();
    }
}
