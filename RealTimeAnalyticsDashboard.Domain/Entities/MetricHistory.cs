using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Domain.Entities;

public class MetricHistory
{
    public int Id { get; set; }
    public MetricType MetricType { get; set; }
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
}