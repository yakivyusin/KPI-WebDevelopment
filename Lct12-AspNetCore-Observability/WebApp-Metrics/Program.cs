namespace WebApp_Metrics;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSingleton<WeatherForecastMetrics>();

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
