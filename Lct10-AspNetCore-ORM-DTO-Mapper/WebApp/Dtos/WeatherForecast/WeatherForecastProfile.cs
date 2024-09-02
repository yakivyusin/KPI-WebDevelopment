using AutoMapper;
using WeatherForecastEntity = DataModel.Entities.WeatherForecast;

namespace WebApp.Dtos.WeatherForecast;

public class WeatherForecastProfile : Profile
{
    public WeatherForecastProfile()
    {
        CreateMap<WeatherForecastEntity, WeatherForecastOutputDto>()
            .ForMember(d => d.TemperatureF, opt => opt.MapFrom(e => 32 + (int)(e.TemperatureC / 0.5556)));

        CreateMap<WeatherForecastCreateDto, WeatherForecastEntity>()
            .ForMember(e => e.Summary, opt => opt.MapFrom(d => d.TemperatureC > 10 ? "Warm" : "Cold"));
    }
}
