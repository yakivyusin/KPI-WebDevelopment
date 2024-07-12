using Common_WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace DataBinding_Minimal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

#if true
            app.MapGet("/w1", (int count) =>
#else
            app.MapGet("/w1", ([FromQuery] int count) =>
#endif
            {
                return WeatherForecast.GenerateRandom(count);
            });

            app.MapGet("/w2", ([FromHeader] int count) =>
            {
                return WeatherForecast.GenerateRandom(count);
            });

#if true
            app.MapPost("/w3", (WeatherForecast data) =>
#else
            app.MapPost("/w3", ([FromBody] WeatherForecast data) =>
#endif
            {
                return Results.Created("/w1", data);
            });

            app.MapPost("/w4", ([FromForm] WeatherForecast data) =>
            {
                return Results.Created("/w1", data);
            }).DisableAntiforgery();

            app.MapGet("/w2-1", ([FromHeader(Name = "X-COUNT")] int count) =>
            {
                return WeatherForecast.GenerateRandom(count);
            });

            app.MapGet("/w5", ([FromQuery] int[] data) =>
            {
                return WeatherForecast.GenerateRandom(data[0]).Skip(data[1]);
            });

            app.MapGet("/w8", ([AsParameters] PagingData data) =>
            {
                return WeatherForecast.GenerateRandom(data.Page * data.PageSize).Skip(data.PageSize * (data.Page - 1)).Take(data.PageSize);
            });

            app.Run();
        }
    }

    public record class PagingData([property: FromQuery] int Page, [property: FromHeader(Name = "PAGE-SIZE")] int PageSize);
}
