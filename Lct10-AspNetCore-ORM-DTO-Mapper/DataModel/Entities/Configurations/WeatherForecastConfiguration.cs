using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Entities.Configurations;

internal class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.TemperatureC)
            .HasColumnName("TemperatureCelsius")
            .HasDefaultValue(10);

        builder.HasData(new WeatherForecast
        {
            Id = 1,
            CityId = 1,
            Date = new DateOnly(2024, 9, 2),
            TemperatureC = 30,
            Summary = "Warm"
        });
    }
}
