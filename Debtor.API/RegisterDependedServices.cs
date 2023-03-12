using System;
using System.Reflection;
using Debtor.Core;
using Debtor.Core.Common;
using Debtor.Core.Common.Mapping;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MongoDB.Driver;

namespace Debtor.API;

public static class RegisterDependentServices
{
    public static IConfiguration? Configuration { get; set; }

    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        Configuration = builder.Configuration;

        var domain = $"https://{Configuration["Auth0:Domain"]}/";
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        
        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });

        var settings = builder.Configuration.GetSection(nameof(MongoDBSettings));
        builder.Services.Configure<MongoDBSettings>(settings);
        builder.Services.AddSingleton<IMongoDBSettings>(ms => ms.GetRequiredService<IOptions<MongoDBSettings>>().Value);
        var connectionString = settings.GetValue<string>(nameof(MongoDBSettings.ConnectionString));
        builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(connectionString));

        builder.Services.RegisterCore();
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });

            //options.AddPolicy("AllowSpecificOrigin",
            //    builder =>
            //    {
            //        builder
            //        .WithOrigins("http://localhost:8080")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials();
            //    });
        });

        builder.Services.AddSwaggerGen();

        return builder;
    }
}
