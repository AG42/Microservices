using System.Web.Http;
using Owin;
using System.Web.Http.Dispatcher;
using ServiceOrder.API.Controllers;
using System.Web.Http.ExceptionHandling;
using ServiceOrder.API.Filters;

namespace ServiceOrder.API
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Services.Replace(typeof(IHttpControllerSelector), new VersionControllerSelector(config));

            config.MapHttpAttributeRoutes();
            UnityConfig.RegisterComponents(config);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new ServiceOrder.API.Filters.ExceptionLogger());
            //added to support runtime controller selection
            appBuilder.UseWebApi(config);
        }
    }
}
