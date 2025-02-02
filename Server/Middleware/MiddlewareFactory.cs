using Server.DependencyInjection;

namespace Server.Middleware;

public delegate IMiddleware MiddlewareFactory(DependencyCollection dependencyCollection);