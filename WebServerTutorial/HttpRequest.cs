namespace WebServerTutorial;

public record HttpRequest(string Method, string Path, string Host, Dictionary<string, string> Headers);