using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace Swagger_Controllers.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WeatherForecastsController : ControllerBase
{
    private static readonly WeatherForecastStorage _storage = new();

    /// <summary>
    /// Returns all weather forecasts.
    /// </summary>
    /// <param name="page">The current page number.</param>
    /// <param name="pageSize">The desired page size.</param>
    /// <returns>Paged weather forecasts.</returns>
    [HttpGet(Name = "GetAllWeatherForecasts")]
    public IEnumerable<WeatherForecast> GetAll(int page = 1, int pageSize = 10) => _storage.GetAll().Skip(pageSize * (page - 1)).Take(pageSize);

    /// <summary>
    /// Returns a weather forecast for a date specified.
    /// </summary>
    /// <param name="date">Date</param>
    /// <returns>Weather forecast</returns>
    /// <response code="200">Returns the found item.</response>
    /// <response code="404">If the item is not found.</response>
    [HttpGet("{date}", Name = "GetWeatherForecastByDate")]
    [ProducesResponseType<WeatherForecast>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(DateTime date)
    {
        var forecast = _storage.Get(DateOnly.FromDateTime(date));

        return forecast != null ? Ok(forecast) : NotFound();
    }

    [HttpPost(Name = "AddWeatherForecast")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public IActionResult Post(WeatherForecast forecast) => CreatedAtAction(nameof(Get), _storage.Add(forecast));
}
