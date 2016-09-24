using System.Collections.Generic;

namespace CustomerInformation.Model.Response
{
    public class CustomerSearchByNameResponse: BaseResponse
    {
        private List<CustomerInformationModel> _customers = new List<CustomerInformationModel>();
        public List<CustomerInformationModel> Customers { get { return _customers; } }
    }
}
    