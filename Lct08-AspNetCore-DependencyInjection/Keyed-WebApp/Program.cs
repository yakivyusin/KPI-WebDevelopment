using Keyed_WebApp.Services;

namespace Keyed_WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IServiceInterface, ServiceA>();
        builder.Services.AddSingleton<IServiceInterface, ServiceB>();

        builder.Services.AddKeyedSingleton<IServiceInterface, ServiceA>("A");
        builder.Services.AddKeyedSingleton<IServiceInterface, ServiceB>("B");

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
