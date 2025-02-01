namespace WebServerTutorial.Server;

public class HttpServerMiddlewareBuilder
{
    private readonly List<Func<IMiddleware>> _middlewares = [];

    public HttpServerMiddlewareBuilder UseMiddleware(IMiddleware middleware)
    {
        _middlewares.Add(() => middleware);

        return this;
    }

    public HttpServerMiddlewareBuilder UseMiddleware<T>() where T : class, IMiddleware
    {
        _middlewares.Add(() => (IMiddleware)DependencyInjection.CreateInstance(typeof(T)));

        return this;
    }

    public List<Func<IMiddleware>> Build()
    {
        return _middlewares;
    }
}
