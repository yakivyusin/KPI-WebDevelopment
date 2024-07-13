using Common_FluentValidation;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace FluentValidation_Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

#if AUTOMATIC
            builder.Services.AddValidatorsFromAssemblyContaining<WeatherForecastValidator>();
            builder.Services.AddFluentValidationAutoValidation();
#endif

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
