using Identity_DataModel;
using Identity_DataModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<DataModelContext>(options => options.UseSqlite("Data Source=sample.db"));
        builder.Services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<DataModelContext>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            options.User.RequireUniqueEmail = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapGroup("account")
            .MapIdentityApi<User>()
            .WithTags("Account");

        app.Run();
    }
}
