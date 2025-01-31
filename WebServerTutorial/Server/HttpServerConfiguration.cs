using WebServerTutorial.Logger;

namespace WebServerTutorial.Server;

public record HttpServerConfiguration (
    ILogger Logger
);