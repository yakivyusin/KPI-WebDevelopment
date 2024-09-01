using Inheritance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inheritance;

public class DataModelContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite("Data Source=sample.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Animal>().UseTptMappingStrategy();
        modelBuilder.Entity<Cat>().HasData(new Cat
        {
            Id = 1,
            Name = "Garfield",
            MeowSound = "Lasagna!"
        });
        modelBuilder.Entity<Dog>().HasData(new Dog
        {
            Id = 2,
            Name = "Patron",
            BarkSound = "Glory to Ukraine!"
        });
    }
}
