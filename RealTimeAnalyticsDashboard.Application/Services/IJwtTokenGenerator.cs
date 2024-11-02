using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(AppUser appUser, IEnumerable<string> roles);
}