﻿using Debtor.API;

var app = WebApplication.CreateBuilder(args)
    .RegisterServices()
    .Build();

app.SetupMiddleware()
    .Run();
