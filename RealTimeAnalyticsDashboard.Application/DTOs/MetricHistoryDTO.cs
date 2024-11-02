using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Application.DTOs;

public class MetricHistoryDTO
{
    public int Id { get; set; }
    public MetricType MetricType { get; set; }
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
}
