using RealTimeAnalyticsDashboard.Application.Common.Models;
using RealTimeAnalyticsDashboard.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealTimeAnalyticsDashboard.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RealTimeAnalyticsDashboard.Application.Common.Utility;

namespace RealTimeAnalyticsDashboard.Infrastructure.Implementations;

public class JwtTokenGenerator(IOptions<JwtOptions> jwtOptions) : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string GenerateToken(AppUser appUser, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);

        var claimList = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, appUser.Id),
            new(JwtRegisteredClaimNames.Email, appUser.Email!),
            new(JwtRegisteredClaimNames.Name, appUser.UserName!)
        };

        // adding roles to claim List
        claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // signing credentials
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            IssuedAt = DateTime.UtcNow,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Expires = DateTime.UtcNow.AddHours(SD.ExpirationTokenHours),
            Subject = new ClaimsIdentity(claimList),
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}