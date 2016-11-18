using CustomerSiteLocation.API.Controllers;
using CustomerSiteLocation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace CustomerSiteLocation.API.Controllers
{
    /// <summary>
    /// Selects which controller to serve up based on HTTP header value
    /// </summary>
    /// <summary>
    /// Selects which controller to serve up based on HTTP header value
    /// </summary>
    public class VersionControllerSelector : DefaultHttpControllerSelector
    {
        HttpConfiguration _config;
        public VersionControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            // Make Runtime dictionary
        }

        #region 

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor controllerDescriptor = null;

            // Get list of all controllers provided by the default selector
            IDictionary<string, HttpControllerDescriptor> controllers = GetControllerMapping();

            // Get all route data
            IHttpRouteData routeData = request.GetRouteData();

            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // check if this route is actually an attribute route
            IEnumerable<IHttpRouteData> attributeSubRoutes = routeData.GetSubRoutes();

            // Get Version from Header/Accept Header
            var apiVersion = GetVersionFromHeader(request);

            // if No Subroutes are there
            if (attributeSubRoutes == null)
            {
                string controllerName = GetControllerNameFromRoute<string>(routeData, "controller");

                if (controllerName == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                string newControllerName = apiVersion == 0 ? controllerName : string.Concat(controllerName, Constants.VersionConcator, apiVersion);

                if (controllers.TryGetValue(newControllerName, out controllerDescriptor))
                {
                    return controllerDescriptor;
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            else
            {
                string newControllerNameSuffix = apiVersion > 0 ? string.Concat(Constants.VersionConcator, apiVersion) : Constants.DefaultControllerName;

                IEnumerable<IHttpRouteData> filteredSubRoutes = attributeSubRoutes.Where(attrRouteData =>
                {
                    HttpControllerDescriptor currentDescriptor = GetControllerDescriptor(attrRouteData);

                    bool match = currentDescriptor.ControllerName.EndsWith(newControllerNameSuffix);

                    if (match && (controllerDescriptor == null))
                    {
                        controllerDescriptor = currentDescriptor;
                    }

                    return match;
                });

                routeData.Values[Constants.MSSubRoutes] = filteredSubRoutes.ToArray();
            }

            return controllerDescriptor;
        }

        private HttpControllerDescriptor GetControllerDescriptor(IHttpRouteData routeData)
        {
            return ((HttpActionDescriptor[])routeData.Route.DataTokens["actions"]).First().ControllerDescriptor;
        }

        //Get a value from the route data, if present.
        private static T GetControllerNameFromRoute<T>(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }

            return default(T);
        }

        private int? GetVersionFromHeader(HttpRequestMessage request)
        {
            int? apiVersion = GetVersionFromRequestHeader(request);

            if (apiVersion == null || apiVersion <= 0)
            {
                return Convert.ToInt16(GetVersionFromAcceptHeaderVersion(request));
            }

            return apiVersion;
        }

        private int? GetVersionFromAcceptHeaderVersion(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept;
            foreach (var mime in accept)
            {
                if (mime.MediaType == Constants.JsonMediaType)
                {
                    var value = mime.Parameters
                                     .Where(v => v.Name.Equals(Constants.VersionParamName, StringComparison.OrdinalIgnoreCase))
                                     .FirstOrDefault();

                    return Convert.ToInt16(value.Value);
                }
            }

            return null;
        }

        private int? GetVersionFromRequestHeader(HttpRequestMessage request)
        {
            IEnumerable<string> values;
            var header = request.Headers;
            if (header.TryGetValues(Constants.VersionParamName, out values))
            {
                foreach (string value in values)
                {
                    int version;
                    if (Int32.TryParse(value, out version))
                    {
                        return version;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
