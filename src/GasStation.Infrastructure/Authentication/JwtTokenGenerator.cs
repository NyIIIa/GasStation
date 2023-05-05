using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GasStation.Application.Common.Interfaces.Authentication;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GasStation.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeService _dateTimeService;
    private readonly JwtSettings _jwtSettings;
    
    public JwtTokenGenerator(IDateTimeService dateTimeService, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeService = dateTimeService;
        _jwtSettings = jwtOptions.Value;
    }
    
    public string GenerateJwtToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256Signature);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("Role", user.Role.Title),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeService.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}