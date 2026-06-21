using Common_WeatherForecast;
using Microsoft.OpenApi;

namespace Swagger_Minimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Weather Forecast API",
                    Description = "An ASP.NET Core Web API for managing weather forecasts"
                };

                return Task.CompletedTask;
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }

        MapWeatherForecasts(app.MapGroup("api/weatherforecasts"));

        app.Run();
    }

    private static RouteGroupBuilder MapWeatherForecasts(RouteGroupBuilder group)
    {
        group.MapGet("/", (int page = 1, int pageSize = 10) => new WeatherForecastStorage().GetAll().Skip(pageSize * (page - 1)).Take(pageSize))
            .WithName("GetAllWeatherForecasts")
            .WithSummary("Returns all weather forecasts.")
            .AddOpenApiOperationTransformer((operation, _, _) =>
            {
                operation.Parameters[0].Description = "The current page number.";
                operation.Parameters[1].Description = "The desired page size.";

                return Task.CompletedTask;
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
