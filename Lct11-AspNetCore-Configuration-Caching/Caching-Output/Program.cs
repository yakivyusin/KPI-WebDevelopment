namespace Caching_Output;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOutputCache(opt =>
        {
            opt.AddBasePolicy(b => b.Expire(TimeSpan.FromSeconds(30)).SetVaryByQuery("*"));
        });

        var app = builder.Build();

        app.MapControllers();
        app.UseOutputCache();

        app.Run();
    }
}
