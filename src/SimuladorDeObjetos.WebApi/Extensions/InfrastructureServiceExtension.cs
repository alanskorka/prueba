using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

namespace SimuladorDeObjetos.WebApi.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SimuladorDb");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Missing connection string");
        }

        services.AddDbContext<DbContext, SimuladorDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IClassModelRepository, ClassModelRepository>();
        services.AddScoped<IAtributteModelRepository, AtributteModelRepository>();
        services.AddScoped<ILocalVarModelRepository, LocalVarModelRepository>();
        services.AddScoped<IMethodCallModelRepository, MethodCallModelRepository>();
        services.AddScoped<IMethodModelRepository, MethodModelRepository>();
        services.AddScoped<IParamModelRepository, ParamModelRepository>();
        services.AddScoped<IInterfaceModelRepository, InterfaceModelRepository>();
        services.AddScoped<IInterfaceMethodModelRepository, InterfaceMethodModelRepository>();

        return services;
    }
}