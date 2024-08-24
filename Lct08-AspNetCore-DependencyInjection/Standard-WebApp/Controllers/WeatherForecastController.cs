using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace Standard_WebApp.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastStorage _storage;

    public WeatherForecastController(IWeatherForecastStorage storage) => _storage = storage;

    [HttpGet]
    public IEnumerable<WeatherForecast> Get() => _storage.GetAll();

    [HttpGet("{date}")]
    public WeatherForecast? Get(DateTime date) => _storage.Get(DateOnly.FromDateTime(date));

    [HttpPost]
    public IActionResult Post(WeatherForecast forecast) => CreatedAtAction(nameof(Get), _storage.Add(forecast));

    [HttpDelete]
    public void Delete() => _storage.DeleteAll();

    [HttpDelete("{date}")]
    public void Delete(DateTime date) => _storage.Delete(DateOnly.FromDateTime(date));
}
