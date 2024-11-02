using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Application.Common.Interfaces;

public interface IMetricHistoryRepository : IRepository<MetricHistory>
{
    Task AddListAsync(IEnumerable<MetricHistory> metricHistories);
}

