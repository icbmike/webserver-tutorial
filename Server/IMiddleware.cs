namespace Server;

public interface IMiddleware
{
    public HttpResponse HandleRequest(HttpRequest request, Func<HttpRequest, HttpResponse> next, HttpServerConfiguration configuration);
}