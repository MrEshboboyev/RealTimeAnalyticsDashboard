using Microsoft.AspNetCore.Identity;
using RealTimeAnalyticsDashboard.Application.Common.Models;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Common.Utility;

namespace RealTimeAnalyticsDashboard.Infrastructure.Implementations;

public class AuthService(UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtTokenGenerator jwtTokenGenerator,
    ISessionService sessionService,
    IAnalyticsHubService analyticsHubService) : IAuthService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly ISessionService _sessionService = sessionService;
    private readonly IAnalyticsHubService _analyticsHubService = analyticsHubService;
    protected ResponseDTO _response = new();

    public async Task<ResponseDTO> LoginAsync(LoginModel loginModel)
    {
        try
        {
            // Fetching user from the database
            var userFromDb = await _userManager.FindByNameAsync(loginModel.UserName)
                ?? throw new Exception("User not found!");

            var result = await _signInManager.PasswordSignInAsync(
                loginModel.UserName,
                loginModel.Password,
                isPersistent: false,
                lockoutOnFailure: true // Enabling lockout on failure
            );

            if (result.IsLockedOut)
                throw new Exception("Your account is locked due to multiple unsuccessful login attempts. Please try again later.");

            if (!result.Succeeded)
                throw new Exception("Username/Password is incorrect!");

            // Fetch user roles
            var userRoles = await _userManager.GetRolesAsync(userFromDb);

            // Generate JWT token
            var generatedToken = _jwtTokenGenerator.GenerateToken(userFromDb, userRoles);

            _response.Result = generatedToken;

            #region Create and Broadcast new Session
            // create a new session
            SessionDTO sessionDTO = new()
            {
                AppUserId = userFromDb.Id,
                StartTime = DateTime.Now
            };

            await _sessionService.CreateSessionAsync(sessionDTO);
            
            // Broadcast the new session to admins in real-time
            await _analyticsHubService.BroadcastSessionUpdateAsync(sessionDTO);
            #endregion
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDTO> RegisterAsync(RegisterModel registerModel)
    {
        try
        {
            var emailOrUsernameExist = await EmailOrUserNameExist(registerModel.Email,
                registerModel.UserName);

            if (emailOrUsernameExist.Result is true)
            {
                throw new Exception("Username/email already exist!");
            }

            // create new AppUser instance
            AppUser user = new()
            {
                FullName = registerModel.FullName,
                Email = registerModel.Email,
                UserName = registerModel.UserName
            };


            // create and added to db user
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Error : {result.Errors.FirstOrDefault()!.Description}");
            }

            // assign role
            await _userManager.AddToRoleAsync(user, SD.Role_User);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    public async Task<ResponseDTO> EmailOrUserNameExist(string email, string username)
    {
        try
        {
            var emailExist = await _userManager.FindByEmailAsync(email);
            var usernameExist = await _userManager.FindByNameAsync(username);

            if (emailExist is null && usernameExist is null)
                _response.Result = true;

            _response.Result = false;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}