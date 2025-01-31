namespace WebServerTutorial.Server;

public record HttpResponse(int StatusCode, string StatusText, Dictionary<string, string> Headers, string Body);