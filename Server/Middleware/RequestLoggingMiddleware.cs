namespace Server.Middleware;

public class RequestLoggingMiddleware : IMiddleware
{
    public HttpResponse HandleRequest(HttpRequest request, Func<HttpRequest, HttpResponse> next, HttpServerConfiguration configuration)
    {
        var (method, path, _) = request;

        var response = next(request);

        var message = $"{DateTime.Now:O} - {method} {path} - {response.StatusCode} {response.StatusText}";

        if (response.StatusCode is >= 200 and < 300)
            configuration.Logger.Info(message);
        else
            configuration.Logger.Error(message);

        return response;
    }
}