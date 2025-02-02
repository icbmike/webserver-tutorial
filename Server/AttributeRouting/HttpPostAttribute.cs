namespace Server.AttributeRouting;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPostAttribute : Attribute, IRouteAttribute
{
    public HttpPostAttribute(string path)
    {
        Path = path;
    }

    public string Path { get; }
}