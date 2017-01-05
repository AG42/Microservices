using OrderSecuredRevenue.Model.Responses;

namespace OrderSecuredRevenue.BusinessLayer.Interface
{
    public interface IOrderSecuredRevenueManager
    {
        OrderSecuredRevenueByOrderNoResponse GetOrderSecuredRevenueByOrderNumber(string companyCode, string orderNo);

        //OrderSecuredRevenueByInvoiceNumberReponse GetOrderSecuredRevenueByInvoiceNumber(string companyCode, string invoiceNumber);
        
            /// <summary>
        /// Gets the order details by order number.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="orderNo">The order no.</param>
        /// <returns>SalesOrderHeadDetails</returns>
        SalesOrderDetailsByOrderNoResponse GetOrderDetailsByOrderNumber(string companyCode, string orderNo);

        OrderTypeByOrderNoResponse GetOrderTypeByOrderNumber(string companyCode, string orderNo);

        OrderDeliveryDateByOrderNoResponse GetOrderDeliveryDateByOrderNumber(string companyCode, string orderNo);
    }
}