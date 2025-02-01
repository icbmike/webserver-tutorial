using WebServerTutorial.Logger;
using WebServerTutorial.Server;

var httpServer = new HttpServer(9000);

await httpServer.Configure(c =>
    {
        c.SetLogger(new ConsoleLogger());
    })
    .ConfigureMiddleware(builder =>
    {
        builder
            .UseMiddleware<RequestLoggingMiddleware>()
            .UseMiddleware(new ControllerMiddleware());

    })
.StartServer();