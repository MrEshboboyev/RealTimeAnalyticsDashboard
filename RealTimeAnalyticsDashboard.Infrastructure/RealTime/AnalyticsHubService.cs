using Microsoft.AspNetCore.SignalR;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;

namespace RealTimeAnalyticsDashboard.Infrastructure.RealTime;

public class AnalyticsHubService(IHubContext<AnalyticsHub> hubContext) : IAnalyticsHubService
{
    private readonly IHubContext<AnalyticsHub> _hubContext = hubContext;

    public async Task<ResponseDTO> BroadcastMetricUpdateAsync(MetricDTO metricDTO)
    {
        var response = new ResponseDTO();
        try
        {
            // Broadcast metric update to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveMetricUpdate", metricDTO);
            response.IsSuccess = true;
            response.Message = "Metric update broadcasted successfully.";
            response.Result = metricDTO;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Failed to broadcast metric update: {ex.Message}";
        }

        return response;
    }

    public async Task<ResponseDTO> BroadcastUserActivityUpdateAsync(UserActivityDTO activityDTO)
    {
        var response = new ResponseDTO();
        try
        {
            // Broadcast user activity update to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveUserActivityUpdate", activityDTO);
            response.IsSuccess = true;
            response.Message = "User activity update broadcasted successfully.";
            response.Result = activityDTO;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Failed to broadcast user activity update: {ex.Message}";
        }

        return response;
    }

    public async Task<ResponseDTO> BroadcastSessionUpdateAsync(SessionDTO sessionDTO)
    {
        var response = new ResponseDTO();
        try
        {
            // Broadcast session update to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveSessionUpdate", sessionDTO);
            response.IsSuccess = true;
            response.Message = "Session update broadcasted successfully.";
            response.Result = sessionDTO;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Failed to broadcast session update: {ex.Message}";
        }

        return response;
    }
}