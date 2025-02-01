using WebServerTutorial.Server;
using WebServerTutorial.Services;

public class RequestLoggingMiddleware : IMiddleware
{
    private readonly WeatherService _weatherService;

    public RequestLoggingMiddleware(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public HttpResponse HandleRequest(HttpRequest request, Func<HttpRequest, HttpResponse> next, HttpServerConfiguration configuration)
    {
        var (method, path, _) = request;

        var response = next(request);

        var message = $"{DateTime.Now:O} - {method} {path} - {response.StatusCode} {response.StatusText} - Weather Forecast: {_weatherService.GetForecast()}";

        if (response.StatusCode is >= 200 and < 300)
            configuration.Logger.Info(message);
        else
            configuration.Logger.Error(message);

        return response;
    }
}