namespace Server.Routing;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPostAttribute : Attribute, IRouteAttribute
{
    public HttpPostAttribute(string path)
    {
        Path = path;
    }

    public string Path { get; }
}