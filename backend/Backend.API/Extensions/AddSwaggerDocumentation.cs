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
            c.SwaggerDoc("User", new OpenApiInfo
            {
                Title = "User API",
                Version = "v1",
                Description = "API for portfolio website",
                Contact = new OpenApiContact
                {
                    Name = "Rian Negreiros Dos Santos",
                    Email = "riannegreiros@gmail.com",
                    Url = new Uri("https://www.riannegreiros.dev"),
                }
            });

            c.SwaggerDoc("Admin", new OpenApiInfo
            {
                Title = "Admin API",
                Version = "v1",
                Description = "API for portfolio website",
                Contact = new OpenApiContact
                {
                    Name = "Rian Negreiros Dos Santos",
                    Email = "riannegreiros@gmail.com",
                    Url = new Uri("https://www.riannegreiros.dev"),
                }
            });


            OpenApiSecurityScheme securitySchema = new()
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
            OpenApiSecurityRequirement securityRequirement = new() { { securitySchema, new[] { "Bearer" } } };
            c.AddSecurityRequirement(securityRequirement);

            c.OperationFilter<SwaggerLogoutOperationFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/Admin/swagger.json", "Admin API v1");
            c.SwaggerEndpoint("/swagger/User/swagger.json", "User API v1");
        });

        return app;
    }
}