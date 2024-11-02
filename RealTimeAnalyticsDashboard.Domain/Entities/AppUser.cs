using Microsoft.AspNetCore.Identity;

namespace RealTimeAnalyticsDashboard.Domain.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public virtual ICollection<Session> Sessions { get; set; }
}

