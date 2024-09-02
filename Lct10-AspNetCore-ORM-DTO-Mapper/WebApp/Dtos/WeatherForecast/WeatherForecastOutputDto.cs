using WebApp.Dtos.City;

namespace WebApp.Dtos.WeatherForecast;

public class WeatherForecastOutputDto
{
    public int Id { get; set; }

    public CityOutputDto City { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string Summary { get; set; } = null!;
}
