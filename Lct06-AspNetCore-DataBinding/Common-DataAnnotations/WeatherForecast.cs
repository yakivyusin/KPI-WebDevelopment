using System.ComponentModel.DataAnnotations;

namespace Common_DataAnnotations
{
    public class WeatherForecast
    {
        [Required]
        public DateOnly? Date { get; set; }

        [Range(-20, 20)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

#if true
        [Length(0, 10)]
#else
        [StringLength(maximumLength: 10, MinimumLength = 0)]
#endif
        [DeniedValues("qwerty")]
        public string? Summary { get; set; }
    }
}
