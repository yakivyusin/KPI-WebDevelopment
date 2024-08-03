using Common_DataAnnotations;

namespace DataAnnotations_Minimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        app.MapPost("/weatherforecast", (WeatherForecast weatherForecast) =>
        {
            return Results.Created();
        }).Validate<WeatherForecast>();

        app.Run();
    }
}
