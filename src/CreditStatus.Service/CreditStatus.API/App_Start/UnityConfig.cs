using System.Web.Http;
using CreditStatus.BusinessLayer;
using CreditStatus.BusinessLayer.Interfaces;
using CreditStatus.DataLayer;
using CreditStatus.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace CreditStatus.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            /* Register all your components with the container here. It is NOT necessary to register your controllers 
             e.g. container.RegisterType<ITestService, TestService>();
             */

            container.RegisterType<ICreditStatusManager, CreditStatusManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
