using ServiceContractManagement.DataLayer;
using ServiceContractManagement.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using ServiceContractManagement.BusinessLayer.Interface;
using ServiceContractManagement.BusinessLayer;

namespace ServiceContractManagement.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IServiceContractManager, ServiceContractManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseContext, DatabaseContext>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}