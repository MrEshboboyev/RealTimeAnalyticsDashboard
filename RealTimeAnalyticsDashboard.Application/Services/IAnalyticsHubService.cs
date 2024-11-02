using RealTimeAnalyticsDashboard.Application.DTOs;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface IAnalyticsHubService
{
    Task<ResponseDTO> BroadcastMetricUpdateAsync(MetricDTO metricDTO); // Sends metric updates to clients
    Task<ResponseDTO> BroadcastUserActivityUpdateAsync(UserActivityDTO activityDTO); // Sends activity updates to clients
    Task<ResponseDTO> BroadcastSessionUpdateAsync(SessionDTO sessionDTO); // Sends session updates to clients
}
