using AutoMapper;
using CityEntity = DataModel.Entities.City;

namespace WebApp.Dtos.City;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityEntity, CityOutputDto>();
    }
}
