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
#if true
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var data = JsonSerializer.Deserialize<WeatherForecast>(body);
#else
            var data = await Request.ReadFromJsonAsync<WeatherForecast>();
#endif

            return Created("/w1", data);
        }

        [HttpPost("/w4")]
        public async Task<IActionResult> PostForm()
        {
            var form = await Request.ReadFormAsync();

            var data = new WeatherForecast
            {
                Date = DateOnly.Parse(form["date"].ToString()),
                TemperatureC = int.Parse(form["temp"].ToString())
            };

            return Created("/w1", data);
        }
    }
}
