using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServerTutorial.Server;

public class HttpServer(int port)
{
    private readonly HttpServerConfigurationBuilder _configurationBuilder = new();
    private readonly HttpServerMiddlewareBuilder _middlewareBuilder = new();

    public async Task StartServer()
    {
        var configuration = _configurationBuilder.Build();
        var middlewares = _middlewareBuilder.Build();

        var ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        using var serverSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        serverSocket.Bind(ipEndPoint);
        serverSocket.Listen();

        while (true)
        {
            try
            {
                using var connectionSocket = await serverSocket.AcceptAsync();

                await HandleConnection(connectionSocket, configuration, middlewares);

                await connectionSocket.DisconnectAsync(false);
            }
            catch (Exception ex)
            {
                configuration.Logger.Error(ex.Message);
            }
        }
    }

    private async Task HandleConnection(Socket connectionSocket, HttpServerConfiguration configuration, List<IMiddleware> middlewares)
    {
        // bytes to text
        var buffer = new ArraySegment<byte>(new byte[2048]);
        var requestText = new StringBuilder();
        var requestHasMoreBytes = true;

        while (requestHasMoreBytes)
        {
            var bytesReceived = await connectionSocket.ReceiveAsync(buffer);
            if (bytesReceived == 0) return;
            requestText.Append(Encoding.UTF8.GetString(buffer[new Range(0, bytesReceived - 1)]));

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

    public HttpServer Configure(Action<HttpServerConfigurationBuilder> builderAction)
    {
        builderAction(_configurationBuilder);

        return this;
    }

    public HttpServer ConfigureMiddleware(Action<HttpServerMiddlewareBuilder> builderAction)
    {
        builderAction(_middlewareBuilder);

        return this;
    }
}