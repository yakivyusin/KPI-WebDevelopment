using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace DataBinding_Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("/w1")]
#if true
    public IEnumerable<WeatherForecast> GetQuery(int count)
#else
    public IEnumerable<WeatherForecast> GetQuery([FromQuery] int count)
#endif
    {
        return WeatherForecast.GenerateRandom(count);
    }

    [HttpGet("/w2")]
    public IEnumerable<WeatherForecast> GetHeader([FromHeader] int count)
    {
        return WeatherForecast.GenerateRandom(count);
    }

    [HttpPost("/w3")]
#if true
    public IActionResult PostBody(WeatherForecast data)
#else
    public IActionResult PostBody([FromBody] WeatherForecast data)
#endif
    {
        return Created("/w1", data);
    }

    [HttpPost("/w4")]
    public IActionResult PostForm([FromForm] WeatherForecast data)
    {
        return Created("/w1", data);
    }

    [HttpGet("/w2-1")]
    public IEnumerable<WeatherForecast> GetHeader1([FromHeader(Name = "X-COUNT")] int count)
    {
        return WeatherForecast.GenerateRandom(count);
    }

    [HttpPost("/w4-1")]
    public IActionResult PostForm1([FromForm] [Bind(Prefix = "dto")] WeatherForecast data)
    {
        return Created("/w1", data);
    }

    [HttpGet("/w5")]
    public IEnumerable<WeatherForecast> GetQueryArray([FromQuery] int[] data)
    {
        return WeatherForecast.GenerateRandom(data[0]).Skip(data[1]);
    }

    [HttpPost("/w6")]
    public IActionResult PostFormList([FromForm] List<WeatherForecast> data)
    {
        return Created("/w1", data);
    }

    [HttpPost("/w7")]
    public IActionResult PostFormDictionary([FromForm] Dictionary<int, WeatherForecast> data)
    {
        return Created("/w1", data);
    }
}
