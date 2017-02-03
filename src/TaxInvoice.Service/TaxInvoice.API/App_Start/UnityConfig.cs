using Microsoft.Practices.Unity;
using System.Web.Http;
using TaxInvoice.BusinessLayer;
using TaxInvoice.BusinessLayer.Interfaces;
using TaxInvoice.DataAccessLayer;
using TaxInvoice.DataAccessLayer.Interface;
using Unity.WebApi;

namespace TaxInvoice.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ITaxInvoiceManager, TaxInvoiceManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}