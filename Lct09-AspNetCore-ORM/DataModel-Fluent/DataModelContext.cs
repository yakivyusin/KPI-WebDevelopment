using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataModel;

public class DataModelContext : DbContext
{
    public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
