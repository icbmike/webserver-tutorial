namespace WebServerTutorial;

public interface IActionResult
{
    HttpResponse Execute(HttpRequest request);
}