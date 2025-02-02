using WebServerTutorial.DependencyInjection;
using WebServerTutorial.Logger;

namespace WebServerTutorial.Server;

public record HttpServerConfiguration(
    DependencyCollection DependencyCollection
)
{
    public ILogger Logger => DependencyCollection.Resolve<ILogger>();
};