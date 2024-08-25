using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

public class DataModelContext : DbContext
{
    public DbSet<WeatherForecast> WeatherForecast { get; set; }

    public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
    {

    }
}
