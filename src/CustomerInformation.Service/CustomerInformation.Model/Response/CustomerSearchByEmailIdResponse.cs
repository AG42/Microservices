using System.Collections.Generic;

namespace CustomerInformation.Model.Response
{
    public class CustomerSearchByEmailIdResponse : BaseResponse
    {
        public List<CustomerInformationModel> Customers { get; } = new List<CustomerInformationModel>();
    }
}
