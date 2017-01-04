using Microsoft.Practices.Unity;
using OrderSecuredRevenue.BusinessLayer;
using OrderSecuredRevenue.BusinessLayer.Interface;
using OrderSecuredRevenue.DataLayer;
using OrderSecuredRevenue.DataLayer.Interfaces;
using System.Web.Http;
using Unity.WebApi;

namespace OrderSecuredRevenue.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IOrderSecuredRevenueManager, OrderSecuredRevenueManager>();
            container.RegisterType<IDatabaseContext, DatabaseContext>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}