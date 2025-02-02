namespace Server;

public class RequestParser
{
    public static HttpRequest ParseRequest(string requestText)
    {
        var requestLines = requestText.Split(Environment.NewLine);

        var firstLine = requestLines.First().Split(" ");

        var method = firstLine[0];
        var path = firstLine[1];

        var headers = requestLines
            .Skip(1)
            .TakeWhile(s => !string.IsNullOrWhiteSpace(s))
            .Select(line => line.Split(": "))
            .ToDictionary(pair => pair[0], pair => pair[1]);

        return new HttpRequest(method, path, headers);
    }
}