using Microsoft.Practices.Unity;
using ServiceOrder.BusinessLayer;
using ServiceOrder.BusinessLayer.Interfaces;
using ServiceOrder.DataLayer;
using ServiceOrder.DataLayer.Adapters;
using ServiceOrder.DataLayer.Entities.Datalake;
using ServiceOrder.DataLayer.Interfaces;
using System.Web.Http;
using ServiceOrder.DataLayer.Entities.IScala;
using Unity.WebApi;

namespace ServiceOrder.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IServiceOrderManager, ServiceOrderManager>();
            container.RegisterType<IDatabaseContext, DatabaseContext>();
            //container.RegisterType<IDatalakeEntities, DatalakeEntities>();
            //container.RegisterType<IDatalakeAdapter, DatalakeAdapter>();
            container.RegisterType<IiScalaEntities, IScalaEntities>();
            container.RegisterType<ISqlAdapter, SqlAdapter>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}