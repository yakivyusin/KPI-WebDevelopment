using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_JWT.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpGet("login")]
    public string Login(string username, string role, [FromQuery] Dictionary<string, string> claims, string audience = "spa_weatherforecast@kpi.ua")
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiop[]asdfghjkl;'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:ZXCVBNM<>"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "weatherforecast@kpi.ua",
            audience: audience,
            claims: (claims ?? []).Select(x => new Claim(x.Key, x.Value))
                .Prepend(new(JwtRegisteredClaimNames.Sub, username))
                .Prepend(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()))
                .Prepend(new(ClaimTypes.Role, role)),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpGet("me")]
    public object Me()
    {
        return User.Identities.Select(x => new
        {
            AuthType = x.AuthenticationType,
            IsAuthentication = x.IsAuthenticated,
            Name = x.Name,
            Claims = x.Claims.Select(c => new
            {
                c.Type,
                c.Value
            })
        });
    }
}
