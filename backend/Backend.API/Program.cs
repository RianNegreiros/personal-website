using Backend.API.Extensions;
using Backend.API.Middlewares;
using Backend.Core.Models;
using Backend.Infrastructure.Caching;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Data.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Redis";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["ClientUrl"]);
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<IdentityDbContext>();
        await context.Database.MigrateAsync();
        await AdminUserSeed.SeedAsync(services.GetRequiredService<UserManager<User>>(), services.GetRequiredService<RoleManager<IdentityRole>>(), builder.Configuration);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

app.UseHealthChecks("/status");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("CorsPolicy");

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();