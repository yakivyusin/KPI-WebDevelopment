using DataModel.Entities;
using DataModel.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Repositories;

internal class WeatherForecastRepository : GenericRepository<WeatherForecast, int>, IWeatherForecastRepository
{
    public WeatherForecastRepository(DataModelContext context) : base(context)
    {
    }

    public override Task<WeatherForecast?> FindAsync(int key) => _context.Set<WeatherForecast>()
        .Include(x => x.City)
        .FirstOrDefaultAsync(x =>  x.Id == key);

    public Task<WeatherForecast?> FindByCityAndDateAsync(string city, DateOnly date) => _context.Set<WeatherForecast>()
        .Include(x => x.City)
        .FirstOrDefaultAsync(x => x.City.Name == city && x.Date == date);
}
