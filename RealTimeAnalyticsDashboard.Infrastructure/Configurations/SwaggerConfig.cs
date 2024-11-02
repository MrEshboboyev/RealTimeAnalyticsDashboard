using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RealTimeAnalyticsDashboard.Infrastructure.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        OpenApiSecurityScheme openApiSecurityScheme = new()
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization string as follows: `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        };

        OpenApiSecurityRequirement openApiSecurityRequirement = new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        };

        services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition(
                name: JwtBearerDefaults.AuthenticationScheme,
                securityScheme: openApiSecurityScheme
            );

            option.AddSecurityRequirement(openApiSecurityRequirement);
        });
    }
}
