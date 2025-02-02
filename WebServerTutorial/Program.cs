using App.Services;
using Server;
using Server.Authentication;
using Server.Logger;
using Server.Middleware;

var httpServer = new HttpServer(9000);

await httpServer
    .ConfigureDependencies(builder =>
    {
        builder.Register<IWeatherService, WeatherService>()
        .SetLogger(new ConsoleLogger())
        .AddAuthentication("dragons", "are_cool");
    })
    .ConfigureMiddleware(builder =>
    {
        builder
            .UseMiddleware<RequestLoggingMiddleware>()
            .UseMiddleware<AuthenticationMiddleware>()
            .UseMiddleware<RoutingControllerMiddleware>();

    })
.StartServer();