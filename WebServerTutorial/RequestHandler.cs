namespace WebServerTutorial;

public class RequestHandler
{
    public static HttpResponse HandleRequest(HttpRequest request)
    {
        var (method, path, _, _) = request;

        

        var controller = new Controller();

        var methodInfo = controller.GetType().GetMethods()
            .SingleOrDefault(info =>
                info.Name.StartsWith(method, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(info.Name.Substring(method.Length), path.Substring(1), StringComparison.InvariantCultureIgnoreCase));


        var response = methodInfo != null
            ? (HttpResponse)methodInfo.Invoke(controller, [request])!
            : new HttpResponse(
                404,
                "Not Found",
                new Dictionary<string, string> { { "Content-Type", "text/plain" } },
                $"Not Found"
            );

        Console.ForegroundColor = response.StatusCode >= 200 && response.StatusCode < 300 ? ConsoleColor.Green: ConsoleColor.Red;
        Console.WriteLine($"{DateTime.Now:O} - {method} {path} - {response.StatusCode} {response.StatusText}");

        return response;
    }
}