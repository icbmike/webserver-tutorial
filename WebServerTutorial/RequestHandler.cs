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

        if (methodInfo != null)
        {
            return (HttpResponse)methodInfo.Invoke(controller, [request])!;
        }

        return new HttpResponse(
            404,
            "Not Found",
            new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            $"Not Found"
        );
    }
}