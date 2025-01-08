namespace WebServerTutorial;

public class RequestHandler
{
    public static HttpResponse HandleRequest(HttpRequest request)
    {
        var (method, path, host, headers) = request;

        return new HttpResponse(
            200,
            "OK",
            new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            $"Request received: {method} {path}"
        );
    }
}