namespace Common_WeatherForecast;

public class WeatherForecastStorage
{
    private static readonly string[] Summaries = [ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" ];
    private static readonly List<WeatherForecast> Data = [];

    static WeatherForecastStorage()
    {
        Data.AddRange(Enumerable.Range(1, 12)
            .SelectMany(month => Enumerable.Range(1, DateTime.DaysInMonth(DateTime.UtcNow.Year, month))
                .Select(day => new WeatherForecast
                {
                    Date = new(DateTime.UtcNow.Year, month, day),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })));
    }

    public IEnumerable<WeatherForecast> GetAll() => Data.OrderBy(x => x.Date);

    public WeatherForecast? Get(DateOnly date) => Data.FirstOrDefault(x => x.Date == date);

    public WeatherForecast Add(WeatherForecast weatherForecast)
    {
        weatherForecast.Date = Data.Max(x => x.Date).AddDays(1);
        Data.Add(weatherForecast);

        return weatherForecast;
    }

    public void DeleteAll() => Data.Clear();

    public void Delete(DateOnly date) => Data.RemoveAll(x => x.Date == date);
}
