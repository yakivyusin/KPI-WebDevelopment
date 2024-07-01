using Common_WeatherForecast;
using System.Text.Json;

namespace HttpContext_Minimal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            app.MapGet("/w1", (HttpRequest request) =>
            {
                var queryParam = request.Query["count"];

                return WeatherForecast.GenerateRandom(int.Parse(queryParam.ToString()));
            });

            app.MapGet("/w2", (HttpRequest request) =>
            {
                var header = request.Headers["COUNT"];

                return WeatherForecast.GenerateRandom(int.Parse(header.ToString()));
            });

            app.MapPost("/w3", async (HttpRequest request) =>
            {
                using var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();

                var data = JsonSerializer.Deserialize<WeatherForecast>(body);

                return Results.Created("/w1", data);
            });

            app.MapPost("/w4", (HttpRequest request) =>
            {
                var data = new WeatherForecast
                {
                    Date = DateOnly.Parse(request.Form["date"].ToString()),
                    TemperatureC = int.Parse(request.Form["temp"].ToString())
                };

                return Results.Created("/w1", data);
            });

            app.Run();
        }
    }
}
