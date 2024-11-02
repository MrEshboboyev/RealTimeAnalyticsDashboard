using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Infrastructure.Data;

namespace RealTimeAnalyticsDashboard.Infrastructure.Repositories;

public class UserActivityRepository(AppDbContext db) : Repository<UserActivity>(db),
    IUserActivityRepository
{
}

