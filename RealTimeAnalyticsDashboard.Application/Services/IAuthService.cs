using RealTimeAnalyticsDashboard.Application.Common.Models;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface IAuthService
{
    Task<ResponseDTO> LoginAsync(LoginModel loginModel);
    Task<ResponseDTO> RegisterAsync(RegisterModel registerModel);
    Task<ResponseDTO> EmailOrUserNameExist(string email, string username);
}