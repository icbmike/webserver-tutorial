using App.Services;
using Server;
using Server.Logger;
using Server.Middleware;

var httpServer = new HttpServer(9000);

await httpServer
    .ConfigureMiddleware(builder =>
    {
        builder
            .UseMiddleware<RequestLoggingMiddleware>()
            .UseMiddleware<RoutingControllerMiddleware>();

    }).ConfigureDependencies(builder =>
    {
        builder.Register<IWeatherService, WeatherService>();
        builder.SetLogger(new ConsoleLogger());
    })
.StartServer();