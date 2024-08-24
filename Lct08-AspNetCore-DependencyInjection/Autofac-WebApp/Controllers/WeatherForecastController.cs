using Autofac.Features.Metadata;
using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace Autofac_WebApp.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly Meta<IWeatherForecastStorage> _storage;

    public WeatherForecastController(Meta<IWeatherForecastStorage> storage) => _storage = storage;

    [HttpGet("meta")]
    public object GetMetadata() => _storage.Metadata;

    [HttpGet]
    public IEnumerable<WeatherForecast> Get() => _storage.Value.GetAll();

    [HttpGet("{date}")]
    public WeatherForecast? Get(DateTime date) => _storage.Value.Get(DateOnly.FromDateTime(date));

    [HttpPost]
    public IActionResult Post(WeatherForecast forecast) => CreatedAtAction(nameof(Get), _storage.Value.Add(forecast));

    [HttpDelete]
    public void Delete() => _storage.Value.DeleteAll();

    [HttpDelete("{date}")]
    public void Delete(DateTime date) => _storage.Value.Delete(DateOnly.FromDateTime(date));
}
