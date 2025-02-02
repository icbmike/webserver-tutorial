namespace WebServerTutorial.Server;

public class HttpServerMiddlewareBuilder
{
    private readonly List<MiddlewareFactory> _middlewares = [];

    public HttpServerMiddlewareBuilder UseMiddleware(IMiddleware middleware)
    {
        _middlewares.Add(_ => middleware);

        return this;
    }

    public HttpServerMiddlewareBuilder UseMiddleware<T>() where T : class, IMiddleware
    {
        _middlewares.Add(d => d.Resolve<T>());

        return this;
    }

    public List<MiddlewareFactory> Build()
    {
        return _middlewares;
    }
}
