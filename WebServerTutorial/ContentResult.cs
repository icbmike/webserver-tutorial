using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace WebServerTutorial;

public class ContentResult<T>(T content) : IActionResult where T : class
{
    public HttpResponse Execute(HttpRequest request)
    {
        string body;
        string contentType;

        var acceptsHeader = request.Headers["Accept"];
        if(acceptsHeader.Contains("application/json"))
        {
            body = JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true });
            contentType = "application/json";
        }

        else if (acceptsHeader.Contains("text/xml"))
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            
            using var sw = new StringWriter();
            using var xmlWriter = XmlWriter.Create(sw);

            xmlSerializer.Serialize(xmlWriter, content);

            body = sw.ToString();
            contentType = "text/xml";
        }
        else
        {
            body = content.ToString();
            contentType = "text/plain";
        }

        return new HttpResponse(
            200,
            "OK",
            new Dictionary<string, string> { { "Content-Type", contentType } },
            body
        );
    }
}