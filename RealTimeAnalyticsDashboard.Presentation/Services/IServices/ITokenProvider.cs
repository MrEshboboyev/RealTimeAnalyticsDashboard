namespace RealTimeAnalyticsDashboard.Presentation.Services.IServices;

public interface ITokenProvider
{
    void SetToken(string token);
    string? GetToken();
    void ClearToken();
}

