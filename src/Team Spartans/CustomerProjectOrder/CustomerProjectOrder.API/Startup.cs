using System.Web.Http;
using Owin;
using Microsoft.Practices.Unity;
using CustomerProjectOrder.API.Resolver;
using CustomerProjectOrder.DataLayer.Interfaces;
using CustomerProjectOrder.DataLayer;
using CustomerProjectOrder.BusinessLayer;
using CustomerProjectOrder.BusinessLayer.Interface;
using System.Web.Http.Dispatcher;
using CustomerProjectOrder.API.Controllers;
using System.Web.Http.ExceptionHandling;
using CustomerProjectOrder.API.Filters;

namespace CustomerProjectOrder.API
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
            config.Services.Add(typeof(IExceptionLogger), new Filters.ExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            appBuilder.UseWebApi(config);

        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<ICustomerProjectOrderManager, CustomerProjectOrderManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDataLayerContext, DataLayerContext>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

        }
    }
}
