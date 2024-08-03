using Common_FluentValidation;
using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidation_Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
#if !AUTOMATIC
        var validator = new WeatherForecastValidator();
        var validationResult = validator.Validate(weatherForecast);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }
#endif
        return Created();
    }
}
