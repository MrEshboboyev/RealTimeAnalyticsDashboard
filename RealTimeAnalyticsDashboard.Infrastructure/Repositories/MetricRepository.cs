using Microsoft.EntityFrameworkCore;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Domain.Enums;
using RealTimeAnalyticsDashboard.Infrastructure.Data;

namespace RealTimeAnalyticsDashboard.Infrastructure.Repositories;

public class MetricRepository(AppDbContext db) : Repository<Metric>(db),
    IMetricRepository
{
    public async Task<Metric?> GetLatestMetricByTypeAsync(MetricType metricType)
    {
        return await dbSet
            .Where(m => m.MetricType == metricType)
            .OrderByDescending(m => m.Timestamp)
            .FirstOrDefaultAsync();
    }
}