namespace Server.Routing;

public delegate HttpResponse RouteHandler(HttpRequest request, Dictionary<string, string> routeParams);