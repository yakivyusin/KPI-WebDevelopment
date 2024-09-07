namespace Caching_Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
