using ServiceOrder.API.Attributes;
using ServiceOrder.BusinessLayer.Interfaces;
using ServiceOrder.Common;
using ServiceOrder.Common.Enum;
using ServiceOrder.Common.Logger;
using ServiceOrder.Model.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceOrder.API.Controllers
{
    [RoutePrefix("api/serviceOrder")]
    public class ServiceOrderV1Controller : BaseController
    {
        readonly IServiceOrderManager serviceOrderManager;
        public ServiceOrderV1Controller(IServiceOrderManager iserviceOrderManager)
        {
            serviceOrderManager = iserviceOrderManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Service Order Service V2...";
        }
    }
}
