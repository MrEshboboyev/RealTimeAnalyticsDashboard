using Microsoft.EntityFrameworkCore;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Infrastructure.Data;

namespace RealTimeAnalyticsDashboard.Infrastructure.Repositories;

public class SessionRepository(AppDbContext db) : Repository<Session>(db),
    ISessionRepository
{
    public async Task<Session?> GetLatestSessionByUserIdAsync(string userId)
    {
        return await dbSet
            .Where(s => s.AppUserId == userId && s.EndTime == null)
            .OrderByDescending(s => s.StartTime)
            .FirstOrDefaultAsync();
    }
}

