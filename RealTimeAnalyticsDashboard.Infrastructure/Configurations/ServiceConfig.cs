﻿using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Infrastructure.Implementations;
using RealTimeAnalyticsDashboard.Infrastructure.Repositories;
using RealTimeAnalyticsDashboard.Infrastructure.Data;
using RealTimeAnalyticsDashboard.Infrastructure.RealTime;

namespace RealTimeAnalyticsDashboard.Infrastructure.Configurations;

public static class ServiceConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // adding lifetimes
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMetricHistoryService, MetricHistoryService>();
        services.AddScoped<IMetricService, MetricService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IUserActivityService, UserActivityService>();

        // realtime
        services.AddScoped<IAnalyticsHubService, AnalyticsHubService>();
        return services;
    }
}
