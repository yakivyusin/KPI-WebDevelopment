using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth_JWT;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = "weatherforecast@kpi.ua",
                ValidateAudience = true,
                ValidAudience = "spa_weatherforecast@kpi.ua",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiop[]asdfghjkl;'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:ZXCVBNM<>"))
            };
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("KyivOnly", policy => policy.RequireClaim("city", "Kyiv"));
        });

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
