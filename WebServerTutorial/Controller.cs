namespace WebServerTutorial;

public class Controller
{
    public HttpResponse GetHello(HttpRequest request)
    {
        return new HttpResponse(
            200,
            "OK",
            new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            $"Hello"
        );
    }

    public HttpResponse GetGoodbye(HttpRequest request)
    {
        return new HttpResponse(
            200,
            "OK",
            new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            $"Goodbye"
        );
    }
}