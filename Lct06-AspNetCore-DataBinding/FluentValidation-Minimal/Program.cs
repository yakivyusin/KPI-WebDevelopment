#define AUTOMATIC

using Common_FluentValidation;
using Common_WeatherForecast;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace FluentValidation_Minimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

#if AUTOMATIC
        builder.Services.AddValidatorsFromAssemblyContaining<WeatherForecastValidator>();
#endif

        var app = builder.Build();

        app.MapPost("/weatherforecast", (WeatherForecast weatherForecast) =>
        {
#if !AUTOMATIC
            var validator = new WeatherForecastValidator();
            var validationResult = validator.Validate(weatherForecast);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
#endif
            return Results.Created();
        })
#if AUTOMATIC
        .AddFluentValidationAutoValidation();
#else
        ;
#endif

        app.Run();
    }
}
