using Common_WeatherForecast;
using Microsoft.OpenApi.Models;

namespace Swagger_Minimal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Weather Forecast API",
                    Description = "An ASP.NET Core Minimal API for managing weather forecasts"
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            MapWeatherForecasts(app.MapGroup("api/weatherforecasts"));

            app.Run();
        }

        private static RouteGroupBuilder MapWeatherForecasts(RouteGroupBuilder group)
        {
            group.MapGet("/", (int page = 1, int pageSize = 10) => new WeatherForecastStorage().GetAll().Skip(pageSize * (page - 1)).Take(pageSize))
                .WithName("GetAllWeatherForecasts")
                .WithSummary("Returns all weather forecasts.")
                .WithOpenApi(operation =>
                {
                    operation.Parameters[0].Description = "The current page number.";
                    operation.Parameters[1].Description = "The desired page size.";

                    return operation;
                });

            group.MapGet("/{date}", (DateTime date) =>
            {
                var forecast = new WeatherForecastStorage().Get(DateOnly.FromDateTime(date));

                return forecast != null ? Results.Ok(forecast) : Results.NotFound();
            })
                .WithName("GetWeatherForecastByDate")
                .WithSummary("Returns a weather forecast for a date specified.")
                .Produces<WeatherForecast>()
                .Produces(StatusCodes.Status404NotFound);

            return group;
        }
    }
}
