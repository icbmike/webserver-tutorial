namespace Server.AttributeRouting;

[AttributeUsage(AttributeTargets.Method)]
public class HttpGetAttribute : Attribute, IRouteAttribute
{
    public HttpGetAttribute(string path)
    {
        Path = path;
    }

    public string Path { get; }
}