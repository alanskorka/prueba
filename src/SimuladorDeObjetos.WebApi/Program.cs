using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Application.DTOs;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SimuladorDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SimuladorDb")));

var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IClassModelRepository, ClassModelRepository>();
builder.Services.AddScoped<IAtributteModelRepository, AtributteModelRepository>();
builder.Services.AddScoped<ILocalVarModelRepository, LocalVarModelRepository>();
builder.Services.AddScoped<IMethodCallModelRepository, MethodCallModelRepository>();
builder.Services.AddScoped<IMethodModelRepository, MethodModelRepository>();
builder.Services.AddScoped<IParamModelRepository, ParamModelRepository>();

builder.Services.AddScoped<IClassModelService, ClassModelService>();
builder.Services.AddScoped<IAttributeModelService, AttributeModelService>();
builder.Services.AddScoped<IMethodModelService, MethodModelService>();
builder.Services.AddScoped<IParamModelService, ParamModelService>();
builder.Services.AddScoped<ILocalVarModelService, LocalVarModelService>();
builder.Services.AddScoped<IMethodCallModelService, MethodCallModelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
