﻿using WebServerTutorial.DependencyInjection;

namespace WebServerTutorial.Server;

public delegate IMiddleware MiddlewareFactory(DependencyCollection dependencyCollection);