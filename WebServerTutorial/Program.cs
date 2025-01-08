using WebServerTutorial;

var httpServer = new HttpServer(9000);

await httpServer.StartServer();
