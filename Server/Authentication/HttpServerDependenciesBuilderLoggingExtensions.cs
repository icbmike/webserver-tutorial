using Server.DependencyInjection;

namespace Server.Authentication;

public static class HttpServerDependenciesBuilderAuthenticationExtensions
{
    public static HttpServerDependenciesBuilder AddAuthentication(this HttpServerDependenciesBuilder builder,
        string username, string password)
    {

        builder.Register(new AuthConfig(username, password));

        return builder;
    }

}