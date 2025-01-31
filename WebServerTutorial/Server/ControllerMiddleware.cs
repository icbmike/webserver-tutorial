using System.Reflection;
using System.Text.Json;
using WebServerTutorial.ActionResults;

namespace WebServerTutorial.Server;

public class ControllerMiddleware : IMiddleware
{
    public HttpResponse HandleRequest(
        HttpRequest request, 
        Func<HttpRequest, HttpResponse> next,
        HttpServerConfiguration configuration
    )
    {
        var (method, path, _) = request;

        var pathSegments = path.Split("/");

        if (pathSegments.Length != 3) return NotFound();

        var controllerType = typeof(RequestHandler).Assembly.GetTypes()
            .Where(type => type.Namespace?.EndsWith("Controllers") ?? false)
            .SingleOrDefault(type => string.Equals(
                type.Name.Substring(0, type.Name.Length - "Controller".Length),
                pathSegments[1],
                StringComparison.InvariantCultureIgnoreCase)
            );

        if (controllerType == null) return NotFound();

        var methodInfo = controllerType.GetMethods()
            .SingleOrDefault(info =>
                info.Name.StartsWith(method, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(
                    info.Name.Substring(method.Length),
                    pathSegments[2],
                    StringComparison.InvariantCultureIgnoreCase
                )
            );

        if (methodInfo == null) return NotFound();

        var controllerInstance = CreateInstance(controllerType);

        var result = methodInfo.Invoke(controllerInstance, [request])!;

        return result switch
        {
            HttpResponse response => response,
            IActionResult actionResult => actionResult.Execute(request),
            _ => new HttpResponse(
                200,
                "OK",
                new Dictionary<string, string> { { "Content-Type", "application/json" } },
                JsonSerializer.Serialize(result)
            )
        };
    }

    private static object? CreateInstance(Type type)
    {
        var constructorInfo = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();

        var parameterInfos = constructorInfo.GetParameters();

        if (parameterInfos.Length == 0) return Activator.CreateInstance(type);

        var parameterInstances = parameterInfos.Select(pInfo => CreateInstance(pInfo.ParameterType)).ToArray();

        return Activator.CreateInstance(type, parameterInstances);
    }

    private static HttpResponse NotFound()
    {
        return new HttpResponse(
            404,
            "Not Found",
            new Dictionary<string, string> { { "Content-Type", "text/plain" } },
            "Not Found"
        );
    }
}