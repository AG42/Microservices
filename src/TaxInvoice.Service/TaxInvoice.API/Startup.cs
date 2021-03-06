﻿using System.Web.Http;
using Owin;
using System.Web.Http.ExceptionHandling;
using TaxInvoice.API.Filters;
using System.Diagnostics.CodeAnalysis;

namespace TaxInvoice.API
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            UnityConfig.RegisterComponents(config);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Services.Add(typeof(IExceptionLogger), new Filters.ExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            //config.EnsureInitialized();
            appBuilder.UseWebApi(config);
        }
    }
}
