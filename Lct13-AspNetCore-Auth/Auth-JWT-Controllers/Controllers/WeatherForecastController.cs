using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth_JWT.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet("auth")]
    [Authorize]
    public IEnumerable<WeatherForecast> AuthenticatedOnlyGet() => Generate();

    [HttpGet("admin")]
    [Authorize(Roles = "admin")]
    public IEnumerable<WeatherForecast> AdminOnlyGet() => Generate();

    [HttpGet("kyiv")]
    [Authorize(Policy = "KyivOnly")]
    public IEnumerable<WeatherForecast> KyivOnlyGet() => Generate();

    private static IEnumerable<WeatherForecast> Generate() => Enumerable.Range(1, 5)
        .Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
}
