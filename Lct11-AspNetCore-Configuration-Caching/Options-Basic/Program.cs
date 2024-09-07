using Options_Basic.Options;

namespace Options_Basic;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.Configure<MySection>(builder.Configuration.GetSection("MySection"));
        builder.Services.AddOptions<MySubsection>().BindConfiguration("MySection:MySubsection");

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
