using CustomerSiteLocation.BusinessLayer;
using CustomerSiteLocation.BusinessLayer.Interface;
using CustomerSiteLocation.DataLayer;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using CustomerSiteLocation.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CustomerSiteLocation.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<ICustomerSiteLocationManager, CustomerSiteLocationManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}