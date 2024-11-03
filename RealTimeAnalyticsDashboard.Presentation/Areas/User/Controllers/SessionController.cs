using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeAnalyticsDashboard.Application.Common.Utility;
using RealTimeAnalyticsDashboard.Application.Services;
using System.Security.Claims;

namespace RealTimeAnalyticsDashboard.Presentation.Areas.User.Controllers;

[Area(SD.Role_User)]
[Authorize(Roles = SD.Role_User)]
public class SessionController(ISessionService sessionService) : Controller
{
    private readonly ISessionService _sessionService = sessionService;

    // GET: User/Session
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _sessionService.GetSessionsByUserIdAsync(userId);

        if (response.IsSuccess)
            return View(response.Result);

        TempData["error"] = response.Message;
        return RedirectToAction("Index", "Home");
    }

    // GET: User/Session/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
        var response = await _sessionService.GetSessionByIdAsync(id);

        if (response.IsSuccess)
            return View(response.Result);

        TempData["error"] = response.Message;
        return RedirectToAction("Index");
    }
}