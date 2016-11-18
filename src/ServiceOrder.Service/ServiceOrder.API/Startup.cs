using System.Web.Http;
using Owin;
using System.Web.Http.Dispatcher;
using ServiceOrder.API.Controllers;
using System.Web.Http.ExceptionHandling;
using ProductInventory.API.Filters;

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

            //added to support runtime controller selection
            appBuilder.UseWebApi(config);
            config.Services.Add(typeof(IExceptionLogger), new ProductInventory.API.Filters.ExceptionLogger()); config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}
