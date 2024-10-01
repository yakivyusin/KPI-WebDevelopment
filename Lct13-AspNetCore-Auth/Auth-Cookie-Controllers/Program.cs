namespace Auth_Cookie;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthentication().AddCookie(options =>
        {
            options.LoginPath = "/Authentication/Login";
            options.AccessDeniedPath = "/Authentication/AccessDenied";
            options.SlidingExpiration = true;
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("KyivOnly", policy => policy.RequireClaim("city", "Kyiv"));
        });

        var app = builder.Build();

        app.UseAuthorization();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
