using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Auth_Cookie;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication().AddCookie(options =>
        {
            options.LoginPath = "/Authentication/Login";
            options.AccessDeniedPath = "/Authentication/AccessDenied";
            options.SlidingExpiration = true;
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("KyivOnly", policy => policy.RequireClaim("city", "Kyiv"));
        });

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        MapAuthGroup(app.MapGroup("/authentication"));
        MapWeatherForecastGroup(app.MapGroup("/weatherforecast"));

        app.Run();
    }

    private static void MapAuthGroup(RouteGroupBuilder routeGroup)
    {
        routeGroup.MapGet("/login", (HttpContext context, string username, string role, string city) =>
        {
            var claimsIdentity = new ClaimsIdentity(
                [new(ClaimTypes.Role, role), new(ClaimTypes.Name, username), new("city", city)],
                CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            return context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        });

        routeGroup.MapGet("/me", (ClaimsPrincipal user) =>
        {
            return user.Identities.Select(x => new
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
        });
    }

    private static void MapWeatherForecastGroup(RouteGroupBuilder routeGroup)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        IEnumerable<WeatherForecast> Generate() => Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
            .ToArray();

        routeGroup.MapGet("/auth", Generate).RequireAuthorization();
        routeGroup.MapGet("/admin", Generate).RequireAuthorization(policyBuilder => policyBuilder.RequireRole("admin"));
        routeGroup.MapGet("/kyiv", Generate).RequireAuthorization("KyivOnly");
    }
}
