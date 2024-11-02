using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Infrastructure.Data;

namespace RealTimeAnalyticsDashboard.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    private readonly AppDbContext _db = db;

    public IMetricRepository Metric { get; private set; } = new MetricRepository(db);
    public ISessionRepository Session { get; private set; } = new SessionRepository(db);
    public IUserActivityRepository UserActivity { get; private set; } = new UserActivityRepository(db);

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}