using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Application.Common.Interfaces;

public interface ISessionRepository : IRepository<Session>
{
    Task<Session?> GetLatestSessionByUserIdAsync(string userId);
}
