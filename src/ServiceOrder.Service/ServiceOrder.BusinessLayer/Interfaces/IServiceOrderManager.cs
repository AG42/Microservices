using ServiceOrder.Model.Responses;

namespace ServiceOrder.BusinessLayer.Interfaces
{
    public interface IServiceOrderManager
    {
        ServiceOrderByCompanyCodeResponse GetServiceOrderByCompanyCode(string companyCode);
        ServiceOrderByServiceOrderNoResponse GetServiceOrderByServiceOrderNo(string companyCode, string serviceOrderNo);
        ServiceOrderStatusByServiceOrderNoResponse GetServiceOrderStatusByServiceOrderNo(string companyCode, string serviceOrderNo);
        ServiceOrderTypeByServiceOrderNoResponse GetServiceOrderTypeByServiceOrderNo(string companyCode, string serviceOrderNo);
        ServiceOrderByInvoiceCustomerCodeResponse GetServiceOrderByInvoiceCustomerCode(string companyCode, string invoiceCustomerCode);
        ServiceOrderByInvoiceNumberResponse GetServiceOrderByInvoiceNumber(string companyCode, string invoiceNumber);
    }
}
