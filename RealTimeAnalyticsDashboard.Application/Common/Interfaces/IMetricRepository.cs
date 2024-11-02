using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Application.Common.Interfaces;

public interface IMetricRepository : IRepository<Metric>
{
    Task<Metric?> GetLatestMetricByTypeAsync(MetricType metricType);
}

