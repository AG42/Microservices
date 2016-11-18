using System.Web.Http;
using Owin;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using CustomerInformation.API.Controllers;
using CustomerInformation.API.Filters;

namespace CustomerInformation.API
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            UnityConfig.RegisterComponents(config);

            config.Services.Replace(typeof(IHttpControllerSelector), new VersionControllerSelector(config));

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Services.Add(typeof(IExceptionLogger), new Filters.ExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            appBuilder.UseWebApi(config);
        }

        //public static void Register(HttpConfiguration config)
        //{


        //    // Other Web API configuration not shown.
        //}
    }
}
