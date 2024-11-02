using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeAnalyticsDashboard.Application.Common.Models;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Presentation.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RealTimeAnalyticsDashboard.Presentation.Controllers;

public class AuthController(IAuthService authService, 
    ITokenProvider tokenProvider,
    SignInManager<AppUser> signInManager) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var response = await _authService.LoginAsync(loginModel);

        if (response.IsSuccess)
        {
            var token = response.Result.ToString();
            // sign in user applied
            await SignInUser(token);

            // set token for user
            _tokenProvider.SetToken(token);

            TempData["success"] = "Login successfully!";
            return RedirectToAction("Index", "Home");
        }

        TempData["error"] = response.Message;

        return View(loginModel);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Error with entered values");
            return View(model);
        }

        var result = await _authService.RegisterAsync(model);

        if (result.IsSuccess)
        {
            TempData["success"] = "Registration successfully!";
            return RedirectToAction(nameof(Login));
        }

        TempData["error"] = result.Message;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _tokenProvider.ClearToken();
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }

        return RedirectToAction("Index", "Home");
    }

    #region Private Methods
    // Sign In User
    private async Task SignInUser(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        // adding claims
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

        identity.AddClaim(new Claim(ClaimTypes.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
    #endregion
}