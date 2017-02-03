using Microsoft.Practices.Unity;
using PurchaseOrderBySupplier.BusinessLayer;
using PurchaseOrderBySupplier.BusinessLayer.Interfaces;
using PurchaseOrderBySupplier.DataLayer;
using PurchaseOrderBySupplier.DataLayer.Interfaces;
using System.Web.Http;
using Unity.WebApi;

namespace PurchaseOrderBySupplier.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IPurchaseOrderBySupplierManager, PurchaseOrderBySupplierManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}