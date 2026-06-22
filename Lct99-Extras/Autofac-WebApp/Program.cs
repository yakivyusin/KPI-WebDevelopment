using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common_WeatherForecast;

namespace Autofac_WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<WeatherForecastStorage>()
                .As<IWeatherForecastStorage>()
                .WithMetadata("Hello", "World")
                .WithMetadata("Glory to", "Ukraine");
        });

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
