namespace Caching_Response;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddResponseCaching();

        var app = builder.Build();

        app.MapControllers();
        app.UseResponseCaching();

        app.Run();
    }
}
