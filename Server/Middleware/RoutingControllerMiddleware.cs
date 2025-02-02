using System.Reflection;
using Server.ActionResults;
using System.Text.Json;
using System.Text.Json.Nodes;
using Server.Routing;

namespace Server.Middleware;

public class RoutingControllerMiddleware : IMiddleware
{
    public HttpResponse HandleRequest(HttpRequest request, Func<HttpRequest, HttpResponse> next, HttpServerConfiguration configuration)
    {
        var (method, path, _, body) = request;

        var attributeType = method switch
        {
            "GET" => typeof(HttpGetAttribute),
            "POST" => typeof(HttpPostAttribute),
            _ => null
        };

        if (attributeType == null)
        {
            return HttpResponse.MethodNotSupported;
        }

        // Match HTTP method
        var methodInfos = Assembly.GetEntryAssembly()!.GetTypes()
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            .Where(info => info.GetCustomAttributes(attributeType).Any())
            .ToList();

        if (!methodInfos.Any())
        {
            return HttpResponse.NotFound;
        }

        // Match the path, trim the leading /
        var methodInfo = methodInfos.SingleOrDefault(info => ((IRouteAttribute)info.GetCustomAttributes(attributeType).First()).Path == path.Substring(1));

        if (methodInfo == null)
        {
            return HttpResponse.NotFound;
        }

        var controllerInstance = configuration.DependencyCollection.Resolve(methodInfo.DeclaringType!);

        var result = InvokeMethod(request, methodInfo, controllerInstance);

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

    private static object InvokeMethod(HttpRequest request, MethodInfo methodInfo, object controllerInstance)
    {
        if (!methodInfo.GetParameters().Any())
        {
            return methodInfo.Invoke(controllerInstance, [])!;
        }

        if (!request.Headers.TryGetValue("Content-Type", out var contentType))
        {
            return HttpResponse.BadRequest;
        }

        if (contentType == "application/json")
        {
            var jsonNode = JsonNode.Parse(request.Body)!.AsObject();

            var parameters = methodInfo.GetParameters()
                .Select(pInfo => Convert.ChangeType(jsonNode[pInfo.Name!.ToLower()].ToString(), pInfo.ParameterType))
                .ToArray();

            return methodInfo.Invoke(controllerInstance, parameters)!;
        }

        return methodInfo.Invoke(controllerInstance, [request])!;
    }
}