using Server;

namespace App.Controllers;

public class GreetingController
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