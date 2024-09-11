using Serilog;

namespace WebApp_Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();

        builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.MapControllers();

        app.Run();
    }
}
