using WebServerTutorial.Logger;

namespace WebServerTutorial.Server;

public class HttpServerConfigurationBuilder
{
    private ILogger? _logger;

    public HttpServerConfigurationBuilder SetLogger(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public HttpServerConfiguration Build()
    {
        return new HttpServerConfiguration(_logger ?? new SilentLogger());
    }
}