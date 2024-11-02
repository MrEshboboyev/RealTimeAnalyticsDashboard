using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Domain.Entities;

public class Metric
{
    public int Id { get; set; }
    public MetricType MetricType { get; set; }
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
}

