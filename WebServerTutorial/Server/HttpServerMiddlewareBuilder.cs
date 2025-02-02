using WebServerTutorial.DependencyInjection;

namespace WebServerTutorial.Server;

public class HttpServerMiddlewareBuilder
{
    private readonly List<Func<DependencyCollection, IMiddleware>> _middlewares = [];

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

    public List<Func<DependencyCollection, IMiddleware>> Build()
    {
        return _middlewares;
    }

}
