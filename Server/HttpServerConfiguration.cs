using Server.DependencyInjection;
using Server.Logger;

namespace Server;

public record HttpServerConfiguration(
    DependencyCollection DependencyCollection
)
{
    public ILogger Logger => DependencyCollection.CanResolve(typeof(ILogger))
        ? DependencyCollection.Resolve<ILogger>()
        : new SilentLogger();
}
