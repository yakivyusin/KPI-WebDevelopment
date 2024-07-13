using Common_WeatherForecast;
using FluentValidation;

namespace Common_FluentValidation
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.TemperatureC).InclusiveBetween(-20, 20);
            RuleFor(x => x.Summary).Length(0, 10).NotEqual("qwerty");

#if false
            RuleFor(x => x.Summary).Must(x => x?.StartsWith('q') ?? true);
#endif
#if false
            RuleFor(x => x.Summary).StartsWith("q");
#endif
#if false
            RuleFor(x => x.Summary).StartsEndsWith("q", "y");
#endif
        }
    }
}
