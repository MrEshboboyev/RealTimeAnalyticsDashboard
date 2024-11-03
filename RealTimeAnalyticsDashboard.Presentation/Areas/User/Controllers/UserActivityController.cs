using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeAnalyticsDashboard.Application.Common.Utility;
using RealTimeAnalyticsDashboard.Application.Services;
using System.Security.Claims;

namespace RealTimeAnalyticsDashboard.Presentation.Areas.User.Controllers;

[Area(SD.Role_User)]
[Authorize]
public class UserActivityController(IUserActivityService userActivityService) : Controller
{
    private readonly IUserActivityService _userActivityService = userActivityService;

    // Endpoint to view activities by user ID (restricted to the logged-in user)
    [HttpGet]
    public async Task<IActionResult> MyActivities()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _userActivityService.GetActivitiesByUserIdAsync(userId);

        if (response.IsSuccess)
            return View(response.Result);

        TempData["error"] = response.Message;
        return RedirectToAction("Index", "Home");
    }
}