using Common_DataAnnotations;

namespace DataAnnotations_Minimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddValidation();

        var app = builder.Build();

        app.MapPost("/weatherforecast", (WeatherForecast weatherForecast) =>
        {
            return Results.Created();
        });

        app.Run();
    }
}
