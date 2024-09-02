namespace WebApp.Dtos.WeatherForecast;

public class WeatherForecastCreateDto
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int CityId { get; set; }
}
