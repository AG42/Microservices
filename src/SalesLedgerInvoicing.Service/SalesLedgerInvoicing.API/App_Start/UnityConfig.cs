using Microsoft.Practices.Unity;
using SalesLedgerInvoicing.BusinessLayer;
using SalesLedgerInvoicing.BusinessLayer.Interface;
using SalesLedgerInvoicing.DataLayer;
using SalesLedgerInvoicing.DataLayer.Interfaces;
using System.Web.Http;
using Unity.WebApi;

namespace SalesLedgerInvoicing.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ISalesLedgerInvoicingManager, SalesLedgerInvoicingManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseContext, DatabaseContext>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}