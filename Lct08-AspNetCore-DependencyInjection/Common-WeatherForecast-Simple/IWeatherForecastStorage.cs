namespace Common_WeatherForecast;

public interface IWeatherForecastStorage
{
    IEnumerable<WeatherForecast> GetAll();
    WeatherForecast? Get(DateOnly date);
    WeatherForecast Add(WeatherForecast weatherForecast);
    void DeleteAll();
    void Delete(DateOnly date);
}
