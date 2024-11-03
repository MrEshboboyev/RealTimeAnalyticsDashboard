using RealTimeAnalyticsDashboard.Application.DTOs;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface ISessionService
{
    Task<ResponseDTO> GetSessionByIdAsync(int sessionId);
    Task<ResponseDTO> GetAllSessionsAsync();
    Task<ResponseDTO> GetActiveSessionByUserIdAsync(string userId);
    Task<ResponseDTO> CreateSessionAsync(SessionDTO sessionDTO);
    Task<ResponseDTO> EndSessionAsync(int sessionId);
    Task<ResponseDTO> GetSessionsByUserIdAsync(string userId);
    Task<ResponseDTO> DeleteSessionAsync(int sessionId);
}
