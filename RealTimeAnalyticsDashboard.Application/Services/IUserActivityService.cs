using RealTimeAnalyticsDashboard.Application.DTOs;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface IUserActivityService
{
    Task<ResponseDTO> GetActivityByIdAsync(int activityId);
    Task<ResponseDTO> GetAllActivitiesAsync();
    Task<ResponseDTO> CreateActivityAsync(UserActivityDTO userActivityDTO);
    Task<ResponseDTO> GetActivitiesBySessionIdAsync(int sessionId);
    Task<ResponseDTO> GetActivitiesByUserIdAsync(string userId);
    Task<ResponseDTO> DeleteActivityAsync(int activityId);
}

