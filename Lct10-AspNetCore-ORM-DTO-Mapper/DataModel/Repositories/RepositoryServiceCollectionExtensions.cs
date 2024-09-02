using DataModel.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DataModel.Repositories;

public static class RepositoryServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) => services
        .AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
        .AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
}
