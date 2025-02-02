namespace Server.ActionResults;

public class OkResult : IActionResult
{
    public HttpResponse Execute(HttpRequest request)
    {
        return new HttpResponse(200, "OK", new Dictionary<string, string>(), "Ok");
    }
}