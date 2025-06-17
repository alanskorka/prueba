using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimuladorDeObjetos.Infrastructure;
using SimuladorDeObjetos.WebApi.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container using extension methods
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Apply database migrations on startup with a retry policy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dbContext = services.GetRequiredService<SimuladorDbContext>();
    var maxRetries = 10;
    var retryDelay = TimeSpan.FromSeconds(5);

    for (var i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Attempting to connect to the database... (Attempt {CurrentAttempt} of {MaxAttempts})", i + 1, maxRetries);
            dbContext.Database.Migrate();
            logger.LogInformation("Database connection successful and migrations applied.");
            break;
        }
        catch (SqlException ex)
        {
            logger.LogWarning(ex, "Database not ready yet. Retrying in {DelaySeconds} seconds...", retryDelay.Seconds);
            if (i == maxRetries - 1)
            {
                logger.LogError("Could not connect to the database after {MaxAttempts} attempts. Application is stopping.", maxRetries);
                throw;
            }

            Thread.Sleep(retryDelay);
        }
    }
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();