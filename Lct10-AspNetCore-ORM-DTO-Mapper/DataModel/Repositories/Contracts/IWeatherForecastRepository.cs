using DataModel.Entities;

namespace DataModel.Repositories.Contracts;

public interface IWeatherForecastRepository : IGenericRepository<WeatherForecast, int>
{
    Task<WeatherForecast?> FindByCityAndDateAsync(string city, DateOnly date);
}
