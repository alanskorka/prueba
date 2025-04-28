using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.Infrastructure.Repositories;
using SimuladorDeObjetos.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SimuladorDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SimuladorDb")));

builder.Services.AddScoped<IClassModelRepository, ClassModelRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
