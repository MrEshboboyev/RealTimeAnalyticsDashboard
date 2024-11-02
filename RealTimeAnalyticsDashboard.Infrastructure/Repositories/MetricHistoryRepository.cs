using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Infrastructure.Data;

namespace RealTimeAnalyticsDashboard.Infrastructure.Repositories;

public class MetricHistoryRepository(AppDbContext db) : Repository<MetricHistory>(db),
    IMetricHistoryRepository
{
    public async Task AddListAsync(IEnumerable<MetricHistory> metricHistories)
    {
        foreach (MetricHistory metricHistory in metricHistories)
        {
            await dbSet.AddAsync(metricHistory);
        }
    }
}