using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Entities;

public class WeatherForecast
{
    [Key]
    public DateOnly Date { get; set; }

    [Column("TemperatureCelsius")]
    public int TemperatureC { get; set; }

    [NotMapped]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [StringLength(50)]
    public string? Summary { get; set; }
}
