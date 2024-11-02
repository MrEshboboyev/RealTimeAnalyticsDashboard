using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Application.DTOs;

public class SessionDTO
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string AppUserId { get; set; }
    public virtual UserDTO UserDTO { get; set; }
    public virtual ICollection<UserActivityDTO> UserActivityDTOs { get; set; }
}
