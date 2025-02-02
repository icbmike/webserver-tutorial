using System.Text;
using Server.Authentication;

namespace Server.Middleware;

public class AuthenticationMiddleware(AuthConfig authConfig) : IMiddleware
{
    public HttpResponse HandleRequest(HttpRequest request, Func<HttpRequest, HttpResponse> next, HttpServerConfiguration configuration)
    {
        if (!request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            return HttpResponse.Unauthorized;
        }

        var authHeaderParts = authHeader.Split(" ");
        var authScheme = authHeaderParts[0];
        var credentials = authHeaderParts[1];

        if (authScheme != "Basic")
        {
            return HttpResponse.Unauthorized;
        }

        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
        var credentialParts = decodedCredentials.Split(":");
        var username = credentialParts[0];
        var password = credentialParts[1];

        if (authConfig.Username != username || authConfig.Password != password)
        {
            return HttpResponse.Unauthorized;
        }

        return next(request);
    }
}