using Microsoft.OpenApi;

namespace Swagger_Controllers;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Weather Forecast API",
                    Description = "An ASP.NET Core Web API for managing weather forecasts"
                };

                return Task.CompletedTask;
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }

        app.MapControllers();

        app.Run();
    }
}
