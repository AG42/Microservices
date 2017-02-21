using System.Web.Http;
using Microsoft.Practices.Unity;
using PurchaseLedger.BusinessLayer;
using PurchaseLedger.BusinessLayer.Interfaces;
using PurchaseLedger.DataLayer;
using PurchaseLedger.DataLayer.Interfaces;
using Unity.WebApi;

namespace PurchaseLedger.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IPurchaseLedgerManager, PurchaseLedgerManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
