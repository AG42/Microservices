using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSiteLocation.Model.Response
{
   public class CustomerSiteLocationsResponse :BaseResponse
    {
        public List<CustomerSiteLocationModel> CustomerSiteLocations { get; set; }
    }
}
