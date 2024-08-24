using Common_WeatherForecast;

namespace Standard_WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

#if !BEST_PRACTICE
        builder.Services.AddTransient<IWeatherForecastStorage, WeatherForecastStorage>();
#else
        builder.Services.AddWeatherForecast();
#endif

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
