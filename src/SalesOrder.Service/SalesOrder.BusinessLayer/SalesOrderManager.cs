using SalesOrder.BusinessLayer.Interfaces;
using System.Linq;
using SalesOrder.Model.Response;
using SalesOrder.DataLayer.Interfaces;
using SalesOrder.Common;
using SalesOrder.Common.Error;

namespace SalesOrder.BusinessLayer
{
    public class SalesOrderManager : ISalesOrderManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public SalesOrderManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }


        /// <summary>
        /// This methode provides sales order details based on company code in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderByCompanyCode(string companyCode)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderByCompanyCode(companyCode);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This methode provides sales order detail based on company code and Sales order number in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="salesOrderNumber">Sales order number as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderBySalesOrderNumber(string companyCode, string salesOrderNumber)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderBySalesOrderNumber(companyCode, salesOrderNumber);
            if (purchaseOrders != null)
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This methode provides sales order details based on company code and type of order in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="orderType">Type of order as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderByOrderType(string companyCode, string orderType)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderByOrderType(companyCode, orderType);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This methode provides sales order details based on company code and customer invoice code in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="customerInvoiceCode">Customer invoice code as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderByCustomerInvoiceCode(string companyCode, string customerInvoiceCode)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderByCustomerInvoiceCode(companyCode, customerInvoiceCode);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This methode provides sales order details based on company code and flag pick list in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="flagPickList">Flag pick list as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderByFlagPickList(string companyCode, string flagPickList)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderByFlagPickList(companyCode, flagPickList);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This methode provides sales order details based on company code and order date range in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="minOrderDate">Minimum order date as string</param>
        /// <param name="maxOrderDate">Maximum order date as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderByOrderDateRange(string companyCode, string minOrderDate, string maxOrderDate)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderByOrderDateRange(companyCode, minOrderDate, maxOrderDate);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This methode provides sales order details based on company code and delivery date range in business layer
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="minDeliveryDate">Minimum delivery date as string</param>
        /// <param name="maxDeliveryDate">Maximum delivery date as string</param>
        /// <returns>Return object of SalesOrdersResponse</returns>
        public SalesOrdersResponse GetSalesOrderByDeliveryDateRange(string companyCode, string minDeliveryDate, string maxDeliveryDate)
        {
            var response = new SalesOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetSalesOrderByDeliveryDateRange(companyCode, minDeliveryDate, maxDeliveryDate);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.SalesOrders = Converter.ConvertToSalesOrderList(purchaseOrders);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
    }
}