using Server.Middleware;

namespace Server.Routing;

public static class HttpServerMiddlewareBuilderRoutingExtensions
{
    public static HttpServerMiddlewareBuilder UseRouter(this HttpServerMiddlewareBuilder builder,
        Action<RouterBuilder> routerBuilderAction)
    {
        var routerBuilder = new RouterBuilder();

        routerBuilderAction(routerBuilder);

        builder.UseMiddleware(routerBuilder.Build());

        return builder;
    }
}
