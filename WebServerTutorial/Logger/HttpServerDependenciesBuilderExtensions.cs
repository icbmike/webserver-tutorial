using WebServerTutorial.DependencyInjection;

namespace WebServerTutorial.Logger;

public static class HttpServerDependenciesBuilderExtensions
{
    public static HttpServerDependenciesBuilder SetLogger(this HttpServerDependenciesBuilder builder, ILogger logger)
    {
        builder.Register(logger);

        return builder;
    }

}