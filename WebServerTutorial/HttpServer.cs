using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServerTutorial;

public class HttpServer(int port)
{
    public async Task StartServer()
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        using var serverSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        serverSocket.Bind(ipEndPoint);
        serverSocket.Listen();

        while (true)
        {
            using var connectionSocket = await serverSocket.AcceptAsync();
           
            await HandleConnection(connectionSocket);

            await connectionSocket.DisconnectAsync(false);
        }
    }

    private async Task HandleConnection(Socket connectionSocket)
    {
        var buffer = new ArraySegment<byte>(new byte[2048]);
        var requestText = new StringBuilder();
        var requestHasMoreBytes = true;

        while (requestHasMoreBytes)
        {
            var bytesReceived = await connectionSocket.ReceiveAsync(buffer);
            requestText.Append(Encoding.UTF8.GetString(buffer[new Range(0, bytesReceived - 1)]));

            requestHasMoreBytes = bytesReceived == 2048;
        }

        var request = RequestParser.ParseRequest(requestText.ToString());
        var response = RequestHandler.HandleRequest(request);
        var responseText = ResponseSerializer.SerializeResponse(response);

        connectionSocket.Send(Encoding.UTF8.GetBytes(responseText));
    }
}