using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;

namespace Caching_Data.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;

    public WeatherForecastController(IMemoryCache memoryCache, IDistributedCache distributedCache)
    {
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    [HttpGet("memory")]
    public IEnumerable<WeatherForecast> GetMemory()
    {
#if true
        var cachedData = _memoryCache.Get<IEnumerable<WeatherForecast>>(nameof(GetMemory));

        if (cachedData != null)
        {
            return cachedData;
        }
        else
        {
            var data = WeatherForecast.GenerateRandom(10);
            _memoryCache.Set(nameof(GetMemory), data, TimeSpan.FromSeconds(30));

            return data;
        }
#else
        return _memoryCache.GetOrCreate<IEnumerable<WeatherForecast>>(nameof(GetMemory), cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);

            return WeatherForecast.GenerateRandom(10);
        })!;
#endif
    }

    [HttpGet("distributed")]
    public async Task<IEnumerable<WeatherForecast>> GetDistributed()
    {
        var cachedData = await _distributedCache.GetAsync(nameof(GetDistributed));

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<WeatherForecast[]>(Encoding.UTF8.GetString(cachedData))!;
        }
        else
        {
            var data = WeatherForecast.GenerateRandom(10);
            _distributedCache.Set(nameof(GetDistributed), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

            return data;
        }
    }
}
