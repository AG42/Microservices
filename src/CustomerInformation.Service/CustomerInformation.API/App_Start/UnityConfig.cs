using CustomerInformation.BusinessLayer;
using CustomerInformation.BusinessLayer.Interface;
using CustomerInformation.DataLayer;
using CustomerInformation.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CustomerInformation.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ICustomerManager, CustomerManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseContext, DatabaseContext>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}