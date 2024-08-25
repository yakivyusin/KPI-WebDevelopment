using DataModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<DataModelContext>(contextOptions =>
            contextOptions.UseSqlite("Data Source=sample.db"));

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
