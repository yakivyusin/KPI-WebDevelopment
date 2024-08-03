using Common_WeatherForecast;

namespace Routing_Minimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        var group = app.MapGroup("/api/weatherforecast");

        group.MapGet("/", () => new WeatherForecastStorage().GetAll());

        group.MapGet("/{date}", (DateTime date) => new WeatherForecastStorage().Get(DateOnly.FromDateTime(date)));

        group.MapGet("/{year}/{month}/{day?}", (int year, int month, int day = 1) => new WeatherForecastStorage().Get(new(year, month, day)));

        group.MapGet("/catch-all/{**rest}", (string rest) => rest);

        group.MapGet("/{year:int:min(2020)}/{month:int}/{day:int?}", (int year, int month, int day = 1) => new WeatherForecastStorage().Get(new(year, month, day)));

        app.Run();
    }
}
