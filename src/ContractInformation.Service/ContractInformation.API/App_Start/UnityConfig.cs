using ContractInformation.BusinessLayer;
using ContractInformation.BusinessLayer.Interfaces;
using ContractInformation.DataLayer;
using ContractInformation.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace ContractInformation.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            /* Register all your components with the container here. It is NOT necessary to register your controllers 
             e.g. container.RegisterType<ITestService, TestService>();
             */

            container.RegisterType<IContractInformationManager, ContractInformationManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}