namespace WebServerTutorial;

public class ResponseSerializer
{
    public static string SerializeResponse(HttpResponse response)
    {
        var (statusCode, statusText, headers, body) = response;

        return $"""
                HTTP/1.0 {statusCode} {statusText}
                {string.Join(Environment.NewLine, headers.Select(kvp =>  $"{kvp.Key}: {kvp.Value}" ))}

                {body}
                """;
    }
}