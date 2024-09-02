using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Entities.Configurations;

internal class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(x => x.WeatherForecasts)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .IsRequired();

        builder.HasData(new City
        {
            Id = 1,
            Name = "Kyiv"
        });
    }
}
