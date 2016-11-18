using Microsoft.Practices.Unity;
using ServiceOrder.BusinessLayer;
using ServiceOrder.BusinessLayer.Interfaces;
using ServiceOrder.DataLayer;
using ServiceOrder.DataLayer.Interfaces;
using System.Web.Http;
using Unity.WebApi;

namespace ServiceOrder.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IServiceOrderManager, ServiceOrderManager>();
            container.RegisterType<IDatabaseContext, DatabaseContext>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}