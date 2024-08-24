using Common_WeatherForecast;

namespace Standard_LifeCyclePlayground;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Try different life cycles and see results in HTTP response
        builder.Services.AddTransient<IWeatherForecastStorage, WeatherForecastStorage>();

        var app = builder.Build();

        app.MapGet("/play", (IWeatherForecastStorage storage1, IWeatherForecastStorage storage2) =>
        {
            return new
            {
                Storage1 = storage1.GetHashCode(),
                Storage2 = storage2.GetHashCode()
            };
        });

        app.Run();
    }
}
