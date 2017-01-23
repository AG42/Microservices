using Microsoft.Practices.Unity;
using OrderedSecuredMargin.BusinessLayer;
using OrderedSecuredMargin.BusinessLayer.Interfaces;
using OrderedSecuredMargin.DataAccessLayer;
using OrderedSecuredMargin.DataAccessLayer.Interface;
using System.Web.Http;
using Unity.WebApi;

namespace OrderedSecuredMargin.API
{
        public static class UnityConfig
        {
            public static void RegisterComponents(HttpConfiguration config)
            {
                var container = new UnityContainer();

                /* Register all your components with the container here. It is NOT necessary to register your controllers 
                 e.g. container.RegisterType<ITestService, TestService>();
                 */

                container.RegisterType<IOrderedSecuredMarginManager, OrderedSecuredMarginManager>();
                container.RegisterType<IDataLayerContext, DataLayerContext>();
                config.DependencyResolver = new UnityDependencyResolver(container);
            }
        }
    }
