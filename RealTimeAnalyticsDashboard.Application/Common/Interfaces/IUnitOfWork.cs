namespace RealTimeAnalyticsDashboard.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IMetricRepository Metric { get; }
    ISessionRepository Session { get; }
    IUserActivityRepository UserActivity { get; }

    Task SaveAsync();
}
