using AutoMapper;
using DataModel.Entities;
using DataModel.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Dtos.WeatherForecast;

namespace WebApp.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWeatherForecastRepository _repository;

    public WeatherForecastController(IMapper mapper, IWeatherForecastRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public Task<List<WeatherForecastOutputDto>> Get() => _mapper.ProjectTo<WeatherForecastOutputDto>(_repository.GetAll())
        .ToListAsync();

    [HttpGet("{id}")]
    public async Task<WeatherForecastOutputDto?> Get(int id)
    {
        var forecast = await _repository.FindAsync(id);

        return _mapper.Map<WeatherForecastOutputDto>(forecast);
    }

    [HttpGet("{city}/{date}")]
    public async Task<WeatherForecastOutputDto?> Get(string city, DateTime date)
    {
        var forecast = await _repository.FindByCityAndDateAsync(city, DateOnly.FromDateTime(date));

        return _mapper.Map<WeatherForecastOutputDto>(forecast);
    }

    [HttpPost]
    public async Task<IActionResult> Post(WeatherForecastCreateDto dto)
    {
        await _repository.AddAsync(_mapper.Map<WeatherForecast>(dto));

        return Created();
    }
}
