using WebServerTutorial.Server;

namespace WebServerTutorial.ActionResults;

public interface IActionResult
{
    HttpResponse Execute(HttpRequest request);
}