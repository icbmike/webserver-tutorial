using App.Services;
using Server;
using Server.ActionResults;

namespace App.Controllers;

public class WeatherController
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public IActionResult GetForecast(HttpRequest request)
    {
        return new ContentResult<ForecastResponse>(new ForecastResponse { Forecast = _weatherService.GetForecast() });
    }
}

public class ForecastResponse
{
    public string Forecast { get; set; }
}