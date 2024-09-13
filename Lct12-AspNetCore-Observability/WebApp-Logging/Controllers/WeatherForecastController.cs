using Microsoft.AspNetCore.Mvc;

namespace WebApp_Logging.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get(int count = 5)
    {
#if true
        _logger.Log(LogLevel.Debug, "Get request for {Count} entities", count);
#else
        _logger.LogDebug("Get request for {Count} entities", count);
#endif

        return Enumerable.Range(1, count).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{id}")]
    public WeatherForecast GetEntity(int id)
    {
        try
        {
            return new()
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on get entity with id = {Id}", id);
            return new();
        }
    }

    [HttpGet("today")]
    public WeatherForecast GetToday()
    {
        _logger.LogDebug(EventIdentifiers.ReadData, "Get today");

        return new()
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };
    }
}
