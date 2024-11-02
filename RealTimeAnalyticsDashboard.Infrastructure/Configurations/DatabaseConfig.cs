using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealTimeAnalyticsDashboard.Infrastructure.Data;

namespace RealTimeAnalyticsDashboard.Infrastructure.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(option =>
        {
            option.UseNpgsql(connectionString);
        });

        return services;
    }
}