using App.Services;
using Server;
using Server.Authentication;
using Server.Logger;
using Server.Middleware;
using Server.Routing;

Movie[] movies = [
    new Movie("Gladiator II", new DateTime(2024, 11, 14), "Ridley Scott"),
    new Movie("Iron Man", new DateTime(2008, 5, 1), "Jon Favreau"),
    new Movie("Barbie", new DateTime(2023, 2, 13), "Greta Gerwig")
];

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
            .UseRouter(router => 
                router.Path("/movies")
                    .Get("", (req) => HttpResponse.Json(movies))
                    .Get("/{name}", (req, reqParams) =>
                    {
                        var movie = movies.FirstOrDefault(movie => movie.Name.Equals(reqParams["name"], StringComparison.InvariantCultureIgnoreCase));

                        return movie == null 
                            ? HttpResponse.NotFound 
                            : HttpResponse.Json(movie);
                    })
            );

    })
.StartServer();

public record Movie(string Name, DateTime ReleaseDate, string Director);
