using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace Routing_Controllers.Controllers;

[ApiController]
[Route("api/weather")]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly WeatherForecastStorage _storage = new();

    [HttpGet]
    [HttpGet("[action]")]
    public IEnumerable<WeatherForecast> Get() => _storage.GetAll();

    [HttpGet("{date}")]
    public WeatherForecast? Get(DateTime date) => _storage.Get(DateOnly.FromDateTime(date));

    [HttpGet("{year}/{month}/{day?}")]
    public WeatherForecast? Get(int year, int month, int day = 1) => _storage.Get(new(year, month, day));

    [HttpGet("catch-all/{**rest}")]
    public string Get(string rest) => rest;

    [HttpGet("{year:int:min(2020)}/{month:int}/{day:int?}")]
    public WeatherForecast? GetConstrained(int year, int month, int day = 1) => _storage.Get(new(year, month, day));
}
