namespace RealTimeAnalyticsDashboard.Domain.Entities;

public class Session
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public virtual ICollection<UserActivity> UserActivities { get; set; }
}