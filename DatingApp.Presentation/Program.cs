//Createing web application instance which is allow to run our application

using System.Reflection;
using DatingApp.Presentation.Extensions;
using DatingApp.Presentation.Middleware;
using DatingApp.Infrastructure.Persistence.Data;
using DatingApp.Infrastructure.Persistence.DependecyInjections;
using DatingApp.Presentation;
using DatingApp.Shared.Interfaces.Requests;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdendityServices(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => !t.IsAbstract && !t.IsInterface &&
                t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IAppRequestHandler<,>)));

foreach (var handlerType in handlerTypes)
{
    builder.Services.AddTransient(handlerType.GetInterfaces().First(), handlerType);
}





var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


//Seeding database
//Migrate database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try 
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}

catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");

}

app.Run();
