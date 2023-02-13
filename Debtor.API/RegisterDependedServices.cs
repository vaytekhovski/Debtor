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

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
        });

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


        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //})
        //    .AddCookie()
        //    .AddOpenIdConnect("Auth0", options =>
        //    {
        //        // Set the authority to the Auth0 domain
        //        options.Authority = $"https://{Configuration["Auth0:Domain"]}";

        //        // Configure Auth0 creds
        //        options.ClientId = Configuration["Auth0:ClientId"];
        //        options.ClientSecret = Configuration["Auth0:ClientSecret"];

        //        // Set response type to code
        //        options.ResponseType = OpenIdConnectResponseType.Code;

        //        // Set scope
        //        options.Scope.Clear();
        //        options.Scope.Add("openid");

        //        options.CallbackPath = new PathString("/callback");

        //        // Set Issuer
        //        options.ClaimsIssuer = "Auth0";

        //        options.Events = new OpenIdConnectEvents
        //        {
        //            // handle the logout redirection
        //            OnRedirectToIdentityProviderForSignOut = (context) =>
        //            {
        //                var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?clienid={Configuration["Auth0:ClientID"]}";

        //                var postLogoutUri = context.Properties.RedirectUri;
        //                if (!string.IsNullOrEmpty(postLogoutUri))
        //                {
        //                    if (postLogoutUri.StartsWith("/"))
        //                    {
        //                        //transform to absolute
        //                        var request = context.Request;
        //                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase;
        //                    }
        //                    logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
        //                }

        //                context.Response.Redirect(logoutUri);
        //                context.HandleResponse();

        //                return Task.CompletedTask;
        //            }
        //        };
        //    });

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

        
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
