using DataModel;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddDbContext<DataModelContext>(contextOptions =>
            contextOptions.UseSqlite("Data Source=sample.db"));
        builder.Services.AddRepositories();

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
