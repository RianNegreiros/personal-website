using System.Reflection;
using Backend.API.Filters;
using Microsoft.OpenApi.Models;

namespace Backend.API.Extensions;
public static class SwaggerServiceExtensions
{
  public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
  {
    services.AddSwaggerGen(c =>
    {
      c.EnableAnnotations();
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "API",
        Version = "v1",
        Description = "API for my portfolio website",
        Contact = new OpenApiContact
        {
          Name = "Rian negreiros Dos Santos",
          Email = "riannegreiros@gmail.com",
          Url = new Uri("https://github.com/RianNegreiros"),
        }
      });

      var securitySchema = new OpenApiSecurityScheme
      {
        Description = "JWT Auth Bearer Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      };
      c.AddSecurityDefinition("Bearer", securitySchema);
      var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
      c.AddSecurityRequirement(securityRequirement);

      c.OperationFilter<SwaggerLogoutOperationFilter>();
    });

    return services;
  }

  public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
  {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

    return app;
  }
}