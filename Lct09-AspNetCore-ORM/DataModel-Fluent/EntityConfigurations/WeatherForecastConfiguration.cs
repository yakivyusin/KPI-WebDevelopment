using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.EntityConfigurations;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(e => e.Date);

        builder.Property(e => e.TemperatureC)
            .HasColumnName("TemperatureCelsius")
            .HasDefaultValue(10);

        builder.Ignore(e => e.TemperatureF);

        builder.Property(e => e.Summary)
            .HasMaxLength(50);
    }
}
