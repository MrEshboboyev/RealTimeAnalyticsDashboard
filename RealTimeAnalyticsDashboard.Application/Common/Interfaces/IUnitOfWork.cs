namespace RealTimeAnalyticsDashboard.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IMetricRepository Metric { get; }
    ISessionRepository Session { get; }
    IUserActivityRepository UserActivity { get; }
    IMetricHistoryRepository MetricHistory { get; }

    Task SaveAsync();
}
