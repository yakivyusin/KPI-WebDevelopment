using System.Diagnostics.Metrics;

namespace WebApp_Metrics;

public class WeatherForecastMetrics
{
    private readonly Meter _meter;
    private readonly Counter<int> _getRequestsCounter;
    private readonly Gauge<int> _todayTemperatureGauge;
    private readonly Histogram<int> _temperatureHistogram;

    public WeatherForecastMetrics(IMeterFactory meterFactory)
    {
        _meter = meterFactory.Create("WebApp_Metrics.WeatherForecast");
        _getRequestsCounter = _meter.CreateCounter<int>("webapp_metrics.weather_forecast.get_requests");
        _todayTemperatureGauge = _meter.CreateGauge<int>("webapp_metrics.weather_forecast.temperature_celsius.today");
        _temperatureHistogram = _meter.CreateHistogram<int>("webapp_metrics.weather_forecast.temperature_celsius");
    }

    public void IncrementGetRequestsCount() => _getRequestsCounter.Add(1);

    public void ReportTemperature(IEnumerable<(DateOnly date, int temperatureC)> list)
    {
        foreach (var item in list)
        {
            _temperatureHistogram.Record(item.temperatureC);

            if (item.date == DateOnly.FromDateTime(DateTime.UtcNow))
            {
                _todayTemperatureGauge.Record(item.temperatureC);
            }
        }
    }
}
