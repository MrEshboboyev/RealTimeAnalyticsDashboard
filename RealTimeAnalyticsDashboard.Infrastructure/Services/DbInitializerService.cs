using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;

namespace RealTimeAnalyticsDashboard.Infrastructure.Services;

public static class DbInitializerService
{
    public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
    }
}
