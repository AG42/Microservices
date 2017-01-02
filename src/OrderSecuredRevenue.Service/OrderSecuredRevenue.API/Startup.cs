using System.Web.Http;
using Owin;
using OrderSecuredRevenue.API.Filters;
using System.Web.Http.ExceptionHandling;
using OrderSecuredRevenue.API;

namespace OrderSecuredRevenue.API
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            UnityConfig.RegisterComponents(config);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
            config.Services.Add(typeof(IExceptionLogger), new OrderSecuredRevenue.API.Filters.ExceptionLogger()); config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}
