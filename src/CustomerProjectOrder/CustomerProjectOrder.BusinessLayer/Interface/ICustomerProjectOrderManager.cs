using CustomerProjectOrder.Model.Response;

namespace CustomerProjectOrder.BusinessLayer.Interface
{
    public interface ICustomerProjectOrderManager
    {
        CustomerProjectOrderResponse GetProjectByNumber(string companyCode, string projectNumber);
        CustomerProjectOrdersResponse GetProjectByCompanyCode(string companyCode);
        CustomerProjectOrdersResponse GetProjectByName(string companyCode, string projectName);
        CustomerProjectOrdersResponse GetProjectByDuration(string companyCode, string startDate ,string endDate);
        CustomerProjectOrderResponse GetProjectByCustomerPONo(string companyCode, string customerPONo);
        CustomerProjectOrdersResponse GetProjectByAccount(string companyCode, string account);

    }
}
