using Server.DependencyInjection;

namespace Server.Logger;

public static class HttpServerDependenciesBuilderLoggingExtensions
{
    public static HttpServerDependenciesBuilder SetLogger(this HttpServerDependenciesBuilder builder, ILogger logger)
    {
        builder.Register(logger);

        return builder;
    }
}