using Server.Middleware;

namespace Server.Routing;

public class RouterMiddleware(
    List<(IRouteMatcher matcher, RouteHandler handler)> getRoutes, 
    IEnumerable<(BaseRouteMatcher matcher, RouterMiddleware)> subRoutes) : IMiddleware
{
    public HttpResponse HandleRequest(HttpRequest request, Func<HttpRequest, HttpResponse> next, HttpServerConfiguration configuration)
    {
        var (method, path, headers, body) = request;

        if (method == "GET")
        {
            var (matcher, routeHandler) = getRoutes.FirstOrDefault(tuple => tuple.matcher.Match(path));

            if (routeHandler != null)
            {
                return routeHandler(request, matcher.GetRouteParams(path));
            }

            var (baseRouteMatcher, routerMiddleware) = subRoutes.FirstOrDefault(tuple => tuple.matcher.Match(path));

            if (routerMiddleware != null)
            {
                return routerMiddleware.HandleRequest(new HttpRequest(method, path.Substring(baseRouteMatcher.BasePath.Length), headers, body), next, configuration);
            }
        }

        return HttpResponse.NotFound;
    }
}