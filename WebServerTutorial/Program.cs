
using System.Net;
using System.Net.Sockets;
using System.Text;


var ipEndPoint = new IPEndPoint(IPAddress.Any, 9000);
using var serverSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

serverSocket.Bind(ipEndPoint);
serverSocket.Listen();

while (true)
{
    using var connectionSocket = serverSocket.Accept();
    Console.WriteLine("Connection accepted");

    var buffer = new ArraySegment<byte>(new byte[2048]);

    var sb = new StringBuilder();

    var requestHasMoreBytes = true;

    while (requestHasMoreBytes)
    {
        var bytesReceived = await connectionSocket.ReceiveAsync(buffer);
        sb.Append(Encoding.UTF8.GetString(buffer[new Range(0, bytesReceived - 1)]));


        requestHasMoreBytes = bytesReceived == 2048;
    }

    Console.WriteLine("Received request:");
    Console.WriteLine(sb.ToString());
    Console.WriteLine("------");


    var responseBytes = """
                        HTTP/1.0 200 OK
                        Content-Type: text/plain

                        Thank you!
                        """u8.ToArray();

    connectionSocket.Send(responseBytes);

}