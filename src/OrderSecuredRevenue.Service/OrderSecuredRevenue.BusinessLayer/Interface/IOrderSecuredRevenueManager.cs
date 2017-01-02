using OrderSecuredRevenue.Model.Responses;

namespace OrderSecuredRevenue.BusinessLayer.Interface
{
    public interface IOrderSecuredRevenueManager
    {
        OrderSecuredRevenueByOrderNoResponse GetOrderSecuredRevenueByOrderNumber(string companyCode, string orderNo);

        //OrderSecuredRevenueByInvoiceNumberReponse GetOrderSecuredRevenueByInvoiceNumber(string companyCode, string invoiceNumber);
    }
}
