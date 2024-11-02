using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RealTimeAnalyticsDashboard.Infrastructure.Configurations;

public static class JwtConfig
{
    public static void AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"] ?? "defaultKeyForJWT");
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = issuer,
            ValidAudience = audience
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
        });
    }
}

