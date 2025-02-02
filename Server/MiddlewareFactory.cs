using Server.DependencyInjection;

namespace Server;

public delegate IMiddleware MiddlewareFactory(DependencyCollection dependencyCollection);