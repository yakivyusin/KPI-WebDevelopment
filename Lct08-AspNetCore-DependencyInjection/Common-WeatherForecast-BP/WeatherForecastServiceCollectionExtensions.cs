using Common_WeatherForecast;

namespace Microsoft.Extensions.DependencyInjection;

public static class WeatherForecastServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherForecast(this IServiceCollection services) =>
        services.AddSingleton<IWeatherForecastStorage, WeatherForecastStorage>();
}
