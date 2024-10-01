using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth_Cookie.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpGet("login")]
    public Task Login(string username, string role, [FromQuery] Dictionary<string, string> claims)
    {
        var claimsIdentity = new ClaimsIdentity(
            (claims ?? []).Select(x => new Claim(x.Key, x.Value)).Prepend(new(ClaimTypes.Role, role)).Prepend(new(ClaimTypes.Name, username)),
            CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        return HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    [HttpGet("me")]
    public object Me()
    {
        return User.Identities.Select(x => new
        {
            x.AuthenticationType,
            x.IsAuthenticated,
            x.Name,
            Claims = x.Claims.Select(c => new
            {
                c.Type,
                c.Value
            })
        });
    }
}
