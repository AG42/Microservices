using Microsoft.Practices.Unity;
using System.Web.Http;
using PurchaseOrder.BusinessLayer;
using PurchaseOrder.BusinessLayer.Interfaces;
using PurchaseOrder.DataLayer;
using PurchaseOrder.DataLayer.Interfaces;
using Unity.WebApi;

namespace PurchaseOrder.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            /* Register all your components with the container here. It is NOT necessary to register your controllers 
             e.g. container.RegisterType<ITestService, TestService>();
             */

            container.RegisterType<IPurchaseOrderManager, PurchaseOrderManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
