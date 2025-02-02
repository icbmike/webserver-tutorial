namespace Server.ActionResults;

public interface IActionResult
{
    HttpResponse Execute(HttpRequest request);
}