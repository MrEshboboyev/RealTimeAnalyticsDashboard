using RealTimeAnalyticsDashboard.Application.Common.Models;
using RealTimeAnalyticsDashboard.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealTimeAnalyticsDashboard.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
            Subject = new ClaimsIdentity(claimList),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}