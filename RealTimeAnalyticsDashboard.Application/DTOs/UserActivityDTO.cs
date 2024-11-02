using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Application.DTOs;

public class UserActivityDTO
{
    public int Id { get; set; }
    public ActivityType ActivityType { get; set; }
    public DateTime Timestamp { get; set; }
    public int SessionId { get; set; }
    public virtual SessionDTO SessionDTO { get; set; }
}
