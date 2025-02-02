using System.Text;

namespace Server;

public class RequestParser
{
    public static HttpRequest ParseRequest(string requestText)
    {
        var requestLines = requestText.Split(Environment.NewLine);

        var firstLine = requestLines.First().Split(" ");

        var method = firstLine[0];
        var path = firstLine[1];

        var isParsingHeaders = true;
        var headers = new Dictionary<string, string>();
        var body = new StringBuilder();

        foreach (var line in requestLines.Skip(1))
        {
            if (isParsingHeaders)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isParsingHeaders = false;
                    continue;
                }

                var splitHeader = line.Split(": ");
                headers.Add(splitHeader[0], splitHeader[1]);
            }
            else
            {
                body.AppendLine(line);
            }
        }

        return new HttpRequest(method, path, headers, body.ToString());
    }
}