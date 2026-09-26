using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Authentication;

public class JwtTokenService(IOptions<JwtOptions> options) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
        new Claim(
            ClaimTypes.NameIdentifier,
            user.Id.ToString()
        ),
        new Claim(
            ClaimTypes.Email,
            user.Email
        )
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.Secret)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _options.AccessTokenMinutes
            ),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

}