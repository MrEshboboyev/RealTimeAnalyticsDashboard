using RealTimeAnalyticsDashboard.Application.Common.Utility;
using RealTimeAnalyticsDashboard.Presentation.Services.IServices;

namespace RealTimeAnalyticsDashboard.Presentation.Services;

public class TokenProvider(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public void ClearToken()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(SD.TokenCookie);
    }

    public string? GetToken()
    {
        bool hasToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.TokenCookie,
            out var token);
        return hasToken ? token : null;
    }

    public void SetToken(string token)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Append(SD.TokenCookie, token);
    }
}

