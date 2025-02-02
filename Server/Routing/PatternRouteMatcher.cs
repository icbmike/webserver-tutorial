using System.Text.RegularExpressions;

namespace Server.Routing;

public class PatternRouteMatcher(string pattern) : IRouteMatcher
{
    private static readonly Regex PatternRegex = new(@"\/{([a-zA-Z0-9]+)}");

    public Dictionary<string, string> GetRouteParams(string path)
    {
        var patternParam = PatternRegex.Match(pattern).Groups[1].Value;

        return new Dictionary<string, string>
        {
            { patternParam , path.Substring(1)}
        };
    }

    public bool Match(string path) => true;
}