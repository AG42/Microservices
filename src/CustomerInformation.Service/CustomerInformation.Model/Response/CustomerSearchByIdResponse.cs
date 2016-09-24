using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.Model.Response
{
    public class CustomerSearchByIdResponse: BaseResponse
    {
        public CustomerInformationModel CustomerInformationModel { get; set; }
    }
}
