using Common_WeatherForecast;
using FluentValidation;

namespace CustomFluentValidations;

public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
{
    public WeatherForecastValidator()
    {
        RuleFor(x => x.Summary).StartsEndsWith("q", "y");
        RuleFor(x => x.Summary).SetValidator(new StartsAndEndsWithPropertyValidator<WeatherForecast>("q", "y"));
    }
}
