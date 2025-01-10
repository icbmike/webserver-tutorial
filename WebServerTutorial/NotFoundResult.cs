namespace WebServerTutorial;

public class NotFoundResult : IActionResult
{
    public HttpResponse Execute(HttpRequest request)
    {
        return new HttpResponse(
            404,
            "Not Found",
            new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            "Not Found"
        );
    }
}