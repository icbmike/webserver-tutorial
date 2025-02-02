using System.Net;
using System.Net.Sockets;
using System.Text;
using Server.DependencyInjection;
using Server.Middleware;

namespace Server;

public class HttpServer(int port)
{
    private readonly HttpServerMiddlewareBuilder _middlewareBuilder = new();
    private readonly HttpServerDependenciesBuilder _dependenciesBuilder = new();

    public async Task StartServer()
    {
        var middlewares = _middlewareBuilder.Build();
        var dependencies = _dependenciesBuilder.Build();

        var configuration = new HttpServerConfiguration(dependencies);

        var ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        using var serverSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        serverSocket.Bind(ipEndPoint);
        serverSocket.Listen();

        while (true)
        {
            //try
            {
                using var connectionSocket = await serverSocket.AcceptAsync();

                await HandleConnection(connectionSocket, configuration, middlewares);

                await connectionSocket.DisconnectAsync(false);
            }
            //catch (Exception ex)
            //{
            //    configuration.Logger.Error(ex.Message);
            //}
        }
    }

    private async Task HandleConnection(Socket connectionSocket, HttpServerConfiguration configuration, List<MiddlewareFactory> middlewares)
    {
        // bytes to text
        var buffer = new ArraySegment<byte>(new byte[2048]);
        var requestText = new StringBuilder();
        var requestHasMoreBytes = true;

        while (requestHasMoreBytes)
        {
            var bytesReceived = await connectionSocket.ReceiveAsync(buffer);
            if (bytesReceived == 0) return;
            requestText.Append(Encoding.UTF8.GetString(buffer[new Range(0, bytesReceived)]));

            requestHasMoreBytes = bytesReceived == 2048;
        }

        // text to C# request
        var request = RequestParser.ParseRequest(requestText.ToString());
        // C# request to C# response
        var response = RequestHandler.HandleRequest(request, configuration, middlewares);
        // C# response to text
        var responseText = ResponseSerializer.SerializeResponse(response);
        // Text to bytes
        connectionSocket.Send(Encoding.UTF8.GetBytes(responseText));
    }

    public HttpServer ConfigureMiddleware(Action<HttpServerMiddlewareBuilder> builderAction)
    {
        builderAction(_middlewareBuilder);

        return this;
    }

    public HttpServer ConfigureDependencies(Action<HttpServerDependenciesBuilder> builderAction)
    {
        builderAction(_dependenciesBuilder);

        return this;
    }
}