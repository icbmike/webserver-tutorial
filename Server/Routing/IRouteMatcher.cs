namespace Server.Routing;

public interface IRouteMatcher
{
    Dictionary<string, string> GetRouteParams(string path);

    bool Match(string path);
}