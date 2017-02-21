using Microsoft.Practices.Unity;
using SalesOrder.BusinessLayer;
using SalesOrder.BusinessLayer.Interfaces;
using SalesOrder.DataLayer;
using SalesOrder.DataLayer.Interfaces;
using System.Web.Http;
using Unity.WebApi;

namespace SalesOrder.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ISalesOrderManager, SalesOrderManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}