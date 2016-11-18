using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace ServiceOrder.API.Controllers
{
    public class VersionController : DefaultHttpControllerSelector
    {
        //store config that gets passed on on startup
        private HttpConfiguration _config;
        //dictionary to hold the list of possible controllers
        private Dictionary<string, HttpControllerDescriptor> _controllers = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

        public VersionController(HttpConfiguration configuration) : base(configuration)
        {
            HttpControllerDescriptor d1 = new HttpControllerDescriptor(configuration, )
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            {

                try

                {

                    string controllerName = base.GetControllerName(request);

                    var controllers = GetControllerMapping();

                    var routeData = request.GetRouteData();

                    string controllerVersion = GetControllerVersionFromRequestHeader(request);

                    controllerName = String.Format("{0}{1}", controllerName, controllerVersion);

                    HttpControllerDescriptor controllerDescriptor;

                    if (!controllers.TryGetValue(controllerName, out controllerDescriptor))
                    {

                        string message = "No HTTP resource was found that matches the specified request URI {0}";

                        throw new HttpResponseException(request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, String.Format(message, request.RequestUri)));

                    }

                    return controllerDescriptor;

                }

                catch (Exception ex)

                {

                    throw new HttpResponseException(request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, String.Format(ex.Message, request.RequestUri)));

                }
            }
        }

        private string GetControllerVersionFromRequestHeader(HttpRequestMessage request)

        {

            var acceptHeader = request.Headers.Accept;

            const string headerName = "Version";

            string controllerVersion = string.Empty;

            if (request.Headers.Contains(headerName))


            {

                controllerVersion = "V" + request.Headers.GetValues(headerName).First();

            }

            return controllerVersion;

        }
    }
}
