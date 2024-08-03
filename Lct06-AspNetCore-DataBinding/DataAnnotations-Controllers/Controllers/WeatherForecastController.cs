using Common_DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DataAnnotations_Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
        return Created();
    }
}
