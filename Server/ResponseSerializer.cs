using System.Text;

namespace Server;

public class ResponseSerializer
{
    public static string SerializeResponse(HttpResponse response)
    {
        var (statusCode, statusText, headers, body) = response;

        var responseBuilder = new StringBuilder();

        responseBuilder.AppendLine($"HTTP/1.0 {statusCode} {statusText}");

        foreach (var headerLine in headers.Select(kvp => $"{kvp.Key}: {kvp.Value}"))
        {
            responseBuilder.AppendLine(headerLine);
        }

        responseBuilder.AppendLine();
        responseBuilder.Append(body);

        return responseBuilder.ToString();
    }
}