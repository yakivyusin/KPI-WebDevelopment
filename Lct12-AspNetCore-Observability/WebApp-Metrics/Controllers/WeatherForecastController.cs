using Microsoft.AspNetCore.Mvc;

namespace WebApp_Metrics.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly WeatherForecastMetrics _metrics;

    public WeatherForecastController(WeatherForecastMetrics metrics) => _metrics = metrics;

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var data = Enumerable.Range(0, 10).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        _metrics.IncrementGetRequestsCount();
        _metrics.ReportTemperature(data.Select(x => (x.Date, x.TemperatureC)));

        return data;
    }
}
