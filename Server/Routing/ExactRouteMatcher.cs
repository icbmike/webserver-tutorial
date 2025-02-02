namespace Server.Routing;

public class ExactRouteMatcher(string exactPath) : IRouteMatcher
{
    public Dictionary<string, string> GetRouteParams(string path)
    {
        return new Dictionary<string, string>();
    }

    public bool Match(string path)
    {
        return path.Equals(exactPath, StringComparison.InvariantCultureIgnoreCase);
    }
}