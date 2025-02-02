namespace Server;

public record HttpRequest(string Method, string Path, Dictionary<string, string> Headers);