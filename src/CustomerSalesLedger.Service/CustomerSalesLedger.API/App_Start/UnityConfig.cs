using CustomerSalesLedger.BusinessLayer;
using CustomerSalesLedger.BusinessLayer.Interfaces;
using CustomerSalesLedger.DataLayer;
using CustomerSalesLedger.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CustomerSalesLedger.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ICustomerSalesLedgerManager, CustomerSalesLedgerManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseContext, DatabaseContext>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}