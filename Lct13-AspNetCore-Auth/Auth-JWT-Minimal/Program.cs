using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_JWT;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = "weatherforecast@kpi.ua",
                ValidateAudience = true,
                ValidAudience = "spa_weatherforecast@kpi.ua",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiop[]asdfghjkl;'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:ZXCVBNM<>"))
            };
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
        routeGroup.MapGet("/login", (string username, string role, string city, string audience = "spa_weatherforecast@kpi.ua") =>
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiop[]asdfghjkl;'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:ZXCVBNM<>"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "weatherforecast@kpi.ua",
                audience: audience,
                claims: [
                    new(JwtRegisteredClaimNames.Sub, username),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role, role),
                    new("city", city)
                ],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        });

        routeGroup.MapGet("/me", (ClaimsPrincipal user) =>
        {
            return user.Identities.Select(x => new
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
