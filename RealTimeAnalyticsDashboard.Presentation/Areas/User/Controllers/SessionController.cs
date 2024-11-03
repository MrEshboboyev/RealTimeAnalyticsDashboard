using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeAnalyticsDashboard.Application.Common.Utility;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;
using System.Security.Claims;

namespace RealTimeAnalyticsDashboard.Presentation.Areas.User.Controllers;

[Area(SD.Role_User)]
[Authorize(Roles = SD.Role_User)]
public class SessionController(ISessionService sessionService, 
    IAnalyticsHubService analyticsHubService) : Controller
{
    private readonly ISessionService _sessionService = sessionService;
    private readonly IAnalyticsHubService _analyticsHubService = analyticsHubService;

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

    // POST: User/Session/Create
    [HttpPost]
    public async Task<IActionResult> CreateSession()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var sessionDTO = new SessionDTO
        {
            AppUserId = userId,
            StartTime = DateTime.Now
        };

        var response = await _sessionService.CreateSessionAsync(sessionDTO);

        if (response.IsSuccess)
        {
            // Notify admins about the new session
            await _analyticsHubService.BroadcastSessionUpdateAsync((SessionDTO)response.Result);
            return RedirectToAction("Index");
        }

        TempData["error"] = response.Message;
        return RedirectToAction("Index");
    }

    // POST: User/Session/End/{id}
    [HttpPost]
    public async Task<IActionResult> EndSession(int id)
    {
        var response = await _sessionService.EndSessionAsync(id);

        if (response.IsSuccess)
        {
            // Notify admins about the ended session
            var sessionDTO = await _sessionService.GetSessionByIdAsync(id);
            if (sessionDTO.IsSuccess)
                await _analyticsHubService.BroadcastSessionUpdateAsync((SessionDTO)sessionDTO.Result);

            return RedirectToAction("Index");
        }

        TempData["error"] = response.Message;
        return RedirectToAction("Index");
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