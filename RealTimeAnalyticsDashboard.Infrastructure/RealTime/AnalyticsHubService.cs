using Microsoft.AspNetCore.SignalR;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;

namespace RealTimeAnalyticsDashboard.Infrastructure.RealTime;

public class AnalyticsHubService(IHubContext<AnalyticsHub> hubContext) : IAnalyticsHubService
{
    private readonly IHubContext<AnalyticsHub> _hubContext = hubContext;
    protected ResponseDTO _response = new();

    public async Task<ResponseDTO> BroadcastMetricUpdateAsync(MetricDTO metricDTO)
    {
        try
        {
            // Broadcast metric update to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveMetricUpdate", metricDTO);
            _response.IsSuccess = true;
            _response.Message = "Metric update broadcasted successfully.";
            _response.Result = metricDTO;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = $"Failed to broadcast metric update: {ex.Message}";
        }

        return _response;
    }

    public async Task<ResponseDTO> BroadcastUserActivityUpdateAsync(UserActivityDTO activityDTO)
    {
        try
        {
            // Broadcast user activity update to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveUserActivityUpdate", activityDTO);
            _response.IsSuccess = true;
            _response.Message = "User activity update broadcasted successfully.";
            _response.Result = activityDTO;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = $"Failed to broadcast user activity update: {ex.Message}";
        }

        return _response;
    }

    public async Task<ResponseDTO> BroadcastSessionUpdateAsync(SessionDTO sessionDTO)
    {
        try
        {
            // Broadcast session update to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveSessionUpdate", sessionDTO);
            _response.IsSuccess = true;
            _response.Message = "Session update broadcasted successfully.";
            _response.Result = sessionDTO;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = $"Failed to broadcast session update: {ex.Message}";
        }

        return _response;
    }
}