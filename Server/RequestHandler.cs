namespace Server;

public class RequestHandler
{
    public static HttpResponse HandleRequest(HttpRequest request, HttpServerConfiguration configuration, List<MiddlewareFactory> middlewares)
    {
        return ExecuteMiddleware(request, middlewares, configuration);
    }

    private static HttpResponse ExecuteMiddleware(HttpRequest request, IEnumerable<MiddlewareFactory> middlewares, HttpServerConfiguration configuration)
    {
        var currentMiddleware = middlewares.FirstOrDefault();
        var rest = middlewares.Skip(1);

        Func<HttpRequest, HttpResponse> nextFunc = rest.Any()
            ? httRequest => ExecuteMiddleware(httRequest, rest, configuration)
            : _ => throw new InvalidOperationException("Last middleware in pipeline shouldn't call next()");

        var response = currentMiddleware(configuration.DependencyCollection).HandleRequest(request, nextFunc, configuration);

        return response;
    }
}

