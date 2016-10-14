using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceOrder.API.Controllers
{
    public class ErrorController: BaseController
    {
        [HttpGet, HttpHead, HttpPost, HttpPut, HttpDelete]
        public HttpResponseMessage Handle404()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);

            response.ReasonPhrase = "The requested resource is not found";

            return response;
        }
    }
}
