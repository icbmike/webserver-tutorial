namespace Server.Routing;

public class BaseRouteMatcher : IRouteMatcher
{
    public string BasePath { get; }

    public BaseRouteMatcher(string basePath)
    {
        BasePath = basePath;
    }

    public Dictionary<string, string> GetRouteParams(string path)
    {
        return new Dictionary<string, string>();
    }

    public bool Match(string path)
    {
        return path.StartsWith(BasePath, StringComparison.InvariantCultureIgnoreCase);
    }
}