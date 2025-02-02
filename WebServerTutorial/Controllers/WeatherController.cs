using WebServerTutorial.ActionResults;
using WebServerTutorial.Server;
using WebServerTutorial.Services;

namespace WebServerTutorial.Controllers;

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