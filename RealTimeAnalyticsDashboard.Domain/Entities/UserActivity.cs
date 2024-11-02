using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Domain.Entities;

public class UserActivity
{
    public int Id { get; set; }
    public ActivityType ActivityType { get; set; }
    public DateTime Timestamp { get; set; }
    public int SessionId { get; set; }
    public virtual Session Session { get; set; }
}