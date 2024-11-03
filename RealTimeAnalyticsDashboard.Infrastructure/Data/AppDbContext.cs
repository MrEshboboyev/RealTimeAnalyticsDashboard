using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<UserActivity> UserActivities { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Metric> Metrics { get; set; }
    public DbSet<MetricHistory> MetricHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure relationships and constraints here
        builder.Entity<Session>()
            .HasOne(s => s.AppUser)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.AppUserId);

        builder.Entity<UserActivity>()
            .HasOne(a => a.Session)
            .WithMany(s => s.UserActivities)
            .HasForeignKey(a => a.SessionId);

        // Configure enum to string conversion
        builder.Entity<Metric>()
            .Property(m => m.MetricType)
            .HasConversion<string>();

        builder.Entity<MetricHistory>()
            .Property(m => m.MetricType)
            .HasConversion<string>();

        builder.Entity<UserActivity>()
            .Property(a => a.ActivityType)
            .HasConversion<string>();

        // DATETIMES
        builder.Entity<Metric>()
            .Property(m => m.Timestamp)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Entity<MetricHistory>()
            .Property(mh => mh.Timestamp)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Entity<Session>()
            .Property(s => s.StartTime)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Remove the default value configuration for EndTime
        builder.Entity<Session>()
            .Property(s => s.EndTime)
            .HasColumnType("timestamp"); // Removed HasDefaultValueSql

        builder.Entity<UserActivity>()
            .Property(ua => ua.Timestamp)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
