namespace Server.Routing;

public class RouterBuilder
{
    private readonly List<(IRouteMatcher matcher, RouteHandler handler)> _getRoutes = new();
    private readonly List<(BaseRouteMatcher matcher, RouterBuilder subBuilder)> _subRoutes = new();

    public RouterBuilder Get(string path, Func<HttpRequest, HttpResponse> handler)
    {
        return Get(path, (req, _) => handler(req));
    }

    public RouterBuilder Get(string path, RouteHandler handler)
    {
        if (path.Contains('{') && path.Contains('}'))
        {
            _getRoutes.Add((new PatternRouteMatcher(path), handler));
        }
        else
        {
            _getRoutes.Add((new ExactRouteMatcher(path), handler));
        }

        return this;
    }

    public RouterBuilder Path(string path)
    {
        var subRouterBuilder = new RouterBuilder();

        _subRoutes.Add((new BaseRouteMatcher(path), subRouterBuilder));

        return subRouterBuilder;
    }

    public RouterMiddleware Build()
    {
        return new RouterMiddleware(
            _getRoutes, 
            _subRoutes.Select(tuple => (tuple.matcher, tuple.subBuilder.Build()))
        );
    }
}