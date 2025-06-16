using System.Text.Json;
using System.Text.Json.Serialization;
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200",
                                "http://localhost:50800",
                                "http://localhost:53177")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("SimuladorDb");
if(string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Missing connection string");
}

services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(
                JsonNamingPolicy.CamelCase,
                allowIntegerValues: false));
    });

services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

services.AddDbContext<DbContext, SimuladorDbContext>(options => options.UseSqlServer(connectionString));

services.AddScoped<IClassModelRepository, ClassModelRepository>();
services.AddScoped<IAtributteModelRepository, AtributteModelRepository>();
services.AddScoped<ILocalVarModelRepository, LocalVarModelRepository>();
services.AddScoped<IMethodCallModelRepository, MethodCallModelRepository>();
services.AddScoped<IMethodModelRepository, MethodModelRepository>();
services.AddScoped<IParamModelRepository, ParamModelRepository>();

services.AddScoped<IClassModelService, ClassModelService>();
services.AddScoped<IAttributeModelService, AttributeModelService>();
services.AddScoped<IMethodModelService, MethodModelService>();
services.AddScoped<IParamModelService, ParamModelService>();
services.AddScoped<ILocalVarModelService, LocalVarModelService>();
services.AddScoped<IMethodCallModelService, MethodCallModelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
