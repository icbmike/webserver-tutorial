namespace WebServerTutorial.Server;

public class HttpServerMiddlewareBuilder
{
    private readonly List<IMiddleware> _middlewares = [];

    public HttpServerMiddlewareBuilder UseMiddleware(IMiddleware middleware)
    {
        _middlewares.Add(middleware);

        return this;
    }

    public List<IMiddleware> Build()
    {
        return _middlewares;
    }
}