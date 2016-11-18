using ProductInventory.BusinessLayer.Interfaces;
using ProductInventory.Common;
using ProductInventory.Common.Enum;
using ProductInventory.Common.Logger;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductInventory.API.Controllers
{

    [RoutePrefix("api/productInventory")]
    public class ProductInventoryV1Controller : ApiController
    {
        readonly IProductInventoryManager _manager;
        public ProductInventoryV1Controller(IProductInventoryManager manager)
        {
            _manager = manager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Product Inventory ProductInventoryV1Controller...";
        }
    }
}