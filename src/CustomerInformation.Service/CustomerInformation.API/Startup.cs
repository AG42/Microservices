using System.Web.Http;
using Owin;
using CustomerInformation.DataLayer.Interfaces;
using CustomerInformation.DataLayer;
using CustomerInformation.BusinessLayer;
using CustomerInformation.BusinessLayer.Interface;

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
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

        //public static void Register(HttpConfiguration config)
        //{
     

        //    // Other Web API configuration not shown.
        //}
    }
}
