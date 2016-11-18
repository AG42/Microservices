using CustomerProjectOrder.API.Resolver;
using CustomerProjectOrder.BusinessLayer;
using CustomerProjectOrder.BusinessLayer.Interface;
using CustomerProjectOrder.DataLayer;
using CustomerProjectOrder.DataLayer.Interfaces;
using CustomerProjectOrder.DataLayer.Entities.Datalake;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace CustomerProjectOrder.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            /* Register all your components with the container here. It is NOT necessary to register your controllers 
             e.g. container.RegisterType<ITestService, TestService>();
             */

            container.RegisterType<ICustomerProjectOrderManager, CustomerProjectOrderManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}