using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeAnalyticsDashboard.Application.Common.Utility;
using RealTimeAnalyticsDashboard.Application.Services;

namespace RealTimeAnalyticsDashboard.Presentation.Areas.Architect.Controllers;

[Area(SD.Role_Architect)]
[Authorize(Roles = SD.Role_Architect)]
public class SessionController(ISessionService sessionService) : Controller
{
    private readonly ISessionService _sessionService = sessionService;

    public async Task<IActionResult> Index()
    {
        var response = await _sessionService.GetAllSessionsAsync();
        
        if (response.IsSuccess)
            return View(response.Result);

        TempData["error"] = response.Message;
        return RedirectToAction("Index","Home");
    }
}

