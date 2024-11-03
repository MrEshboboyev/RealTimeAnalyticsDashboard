using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeAnalyticsDashboard.Application.Common.Utility;
using RealTimeAnalyticsDashboard.Application.Services;

namespace RealTimeAnalyticsDashboard.Presentation.Areas.Architect.Controllers;

[Area(SD.Role_Architect)]
[Authorize(Roles = SD.Role_Architect)]
public class UserActivityController(IUserActivityService userActivityService) : Controller
{
    private readonly IUserActivityService _userActivityService = userActivityService;

    // Endpoint to view all activities
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _userActivityService.GetAllActivitiesAsync();

        if (response.IsSuccess)
            return View(response.Result);

        TempData["error"] = response.Message;
        return RedirectToAction("Index", "Home");
    }

    // Endpoint to delete a specific activity by ID
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _userActivityService.DeleteActivityAsync(id);

        if (response.IsSuccess)
            return RedirectToAction("Index");

        TempData["error"] = response.Message;
        return RedirectToAction("Index");
    }
}