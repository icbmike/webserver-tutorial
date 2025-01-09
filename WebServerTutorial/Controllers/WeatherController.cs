namespace WebServerTutorial.Controllers;

public class WeatherController
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public HttpResponse GetForecast(HttpRequest request)
    {
        return new HttpResponse(
            200, 
            "OK", 
            new Dictionary<string, string> { { "Content-Type", "text/plain" } }, 
            _weatherService.GetForecast()
        );
    }
}