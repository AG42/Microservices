using Microsoft.Practices.Unity;
using ProductInventory.BusinessLayer;
using ProductInventory.BusinessLayer.Interfaces;
using System.Web.Http;
using ProductInventory.DataLayer;
using ProductInventory.DataLayer.Interfaces;
using Unity.WebApi;

namespace ProductInventory.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IProductInventoryManager, ProductInventoryManager>();
            container.RegisterType<IDataLayerContext, DataLayerContext>();
            
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}