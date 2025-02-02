namespace Server;

public record HttpResponse(int StatusCode, string StatusText, Dictionary<string, string> Headers, string Body)
{
    public HttpResponse(int statusCode, string statusText) 
        : this(statusCode, statusText, new(), statusText){}


    public static HttpResponse Ok => new(200, "Ok");
    public static HttpResponse BadRequest => new(400, "Bad request");
    public static HttpResponse Unauthorized => new(401, "Unauthorized");
    public static HttpResponse NotFound => new(404, "Not found");
    public static HttpResponse MethodNotSupported => new(405, "Method not supported");
}