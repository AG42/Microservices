using Microsoft.Practices.Unity;
using ProductInventory.BusinessLayer;
using ProductInventory.BusinessLayer.Interfaces;
using System.Web.Http;
using ProductInventory.DataLayer;
using ProductInventory.DataLayer.Adapters;
using ProductInventory.DataLayer.Entities.Datalake;
using ProductInventory.DataLayer.Interfaces;
using Unity.WebApi;
using System.Web.Http.ExceptionHandling;
using ProductInventory.API.Controllers;

namespace ProductInventory.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IProductInventoryManager, ProductInventoryManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseContext, DatabaseContext>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
            config.Services.Add(typeof(IExceptionLogger), new Filters.ExceptionLogger()); config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}