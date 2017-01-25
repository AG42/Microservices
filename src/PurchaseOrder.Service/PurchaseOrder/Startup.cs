using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Owin;
using PurchaseOrder.App_Start;
using PurchaseOrder.Filters;

namespace PurchaseOrder
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
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Services.Add(typeof(IExceptionLogger), new Filters.ExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            appBuilder.UseWebApi(config);
        }
    }
}
