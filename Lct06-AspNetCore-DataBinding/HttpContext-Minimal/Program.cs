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
#if true
                using var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();

                var data = JsonSerializer.Deserialize<WeatherForecast>(body);
#else
                var data = await request.ReadFromJsonAsync<WeatherForecast>();
#endif

                return Results.Created("/w1", data);
            });

            app.MapPost("/w4", async (HttpRequest request) =>
            {
                var form = await request.ReadFormAsync();

                var data = new WeatherForecast
                {
                    Date = DateOnly.Parse(form["date"].ToString()),
                    TemperatureC = int.Parse(form["temp"].ToString())
                };

                return Results.Created("/w1", data);
            });

            app.Run();
        }
    }
}
