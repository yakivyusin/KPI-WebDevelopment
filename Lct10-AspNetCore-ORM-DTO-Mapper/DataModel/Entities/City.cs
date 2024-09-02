namespace DataModel.Entities;

public class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<WeatherForecast> WeatherForecasts { get; set; } = [];
}
