using WebServerTutorial.Logger;
using WebServerTutorial.Server;
using WebServerTutorial.Services;

var httpServer = new HttpServer(9000);

await httpServer
    .ConfigureMiddleware(builder =>
    {
        builder
            .UseMiddleware<RequestLoggingMiddleware>()
            .UseMiddleware(new ControllerMiddleware());

    }).ConfigureDependencies(builder =>
    {
        builder.Register<IWeatherService, WeatherService>();
        builder.SetLogger(new ConsoleLogger());
    })
.StartServer();