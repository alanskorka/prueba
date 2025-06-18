using SimuladorDeObjetos.Application;
using SimuladorDeObjetos.Application.Interfaces;
using SimuladorDeObjetos.Application.Services;
using SimuladorDeObjetos.Application.Services.Interfaces;

namespace SimuladorDeObjetos.WebApi.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClassModelService, ClassModelService>();
        services.AddScoped<Application.Services.Interfaces.IAttributeModelService, Application.Services.AttributeModelService>();
        services.AddScoped<IMethodModelService, MethodModelService>();
        services.AddScoped<IParamModelService, ParamModelService>();
        services.AddScoped<ILocalVarModelService, LocalVarModelService>();
        services.AddScoped<IMethodCallModelService, MethodCallModelService>();

        return services;
    }
}