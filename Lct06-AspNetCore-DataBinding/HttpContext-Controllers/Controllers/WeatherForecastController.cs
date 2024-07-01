using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HttpContext_Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("/w1")]
        public IEnumerable<WeatherForecast> GetQuery()
        {
            var queryParam = Request.Query["count"];

            return WeatherForecast.GenerateRandom(int.Parse(queryParam.ToString()));
        }

        [HttpGet("/w2")]
        public IEnumerable<WeatherForecast> GetHeader()
        {
            var header = Request.Headers["COUNT"];

            return WeatherForecast.GenerateRandom(int.Parse(header.ToString()));
        }

        [HttpPost("/w3")]
        public async Task<IActionResult> PostBody()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var data = JsonSerializer.Deserialize<WeatherForecast>(body);

            return Created("/w1", data);
        }

        [HttpPost("/w4")]
        public IActionResult PostForm()
        {
            var data = new WeatherForecast
            {
                Date = DateOnly.Parse(Request.Form["date"].ToString()),
                TemperatureC = int.Parse(Request.Form["temp"].ToString())
            };

            return Created("/w1", data);
        }
    }
}
