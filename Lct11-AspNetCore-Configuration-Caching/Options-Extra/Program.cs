using Microsoft.Extensions.Options;
using Options_Extra.Options;

namespace Options_Extra;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSingleton<IValidateOptions<MySection>, MySectionValidator>();
        builder.Services.AddOptions<MySection>()
            .BindConfiguration("MySection")
            .Configure(options =>
            {
                options.KeyC = !options.KeyC;
            })
            .ValidateOnStart();

        builder.Services.AddOptions<MySubsection>()
            .BindConfiguration("MySection:MySubsection")
            .Validate<IOptionsMonitor<MySection>>((options, sectionOptions) =>
            {
                var section = sectionOptions.CurrentValue;

                if (section.KeyC)
                {
                    return options.KeyB.Length > options.KeyA;
                }

                return true;
            }, "If KeyC is enabled then KeyB length must be greater then KeyA");

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
