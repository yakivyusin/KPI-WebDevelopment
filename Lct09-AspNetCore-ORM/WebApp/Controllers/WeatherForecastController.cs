using DataModel;
using DataModel.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly DataModelContext _context;

    public WeatherForecastController(DataModelContext context) => _context = context;

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get() => await _context.Set<WeatherForecast>()
        .ToListAsync();

    [HttpGet("{date}")]
    public async Task<WeatherForecast?> Get(DateTime date) => await _context.Set<WeatherForecast>()
        .FindAsync(DateOnly.FromDateTime(date));

    [HttpPost]
    public async Task<IActionResult> Post(WeatherForecast forecast)
    {
        _context.Set<WeatherForecast>().Add(forecast);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), forecast);
    }

    [HttpDelete]
    public async Task Delete() => await _context.Set<WeatherForecast>().ExecuteDeleteAsync();

    [HttpDelete("{date}")]
    public async Task Delete(DateTime date)
    {
        var entity = await _context.Set<WeatherForecast>()
            .FindAsync(DateOnly.FromDateTime(date));

        if (entity != null)
        {
            _context.Set<WeatherForecast>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
