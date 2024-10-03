namespace FormEchoApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        app.MapPost("/echo", async (HttpRequest request) =>
        {
            var form = await request.ReadFormAsync();

            return string.Join(Environment.NewLine, form);
        });

        app.Run();
    }
}
