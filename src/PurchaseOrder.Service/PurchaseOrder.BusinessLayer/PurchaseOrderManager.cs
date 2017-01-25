using PurchaseOrder.BusinessLayer.Interfaces;
using PurchaseOrder.Common;
using PurchaseOrder.Common.Error;
using PurchaseOrder.DataLayer.Interfaces;
using PurchaseOrder.Model.Response;
using System.Linq;

namespace PurchaseOrder.BusinessLayer
{
    public class PurchaseOrderManager : IPurchaseOrderManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public PurchaseOrderManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }


        /// <summary>
        /// Gets the Purchase order details by company code at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <returns></returns>
        public PurchaseOrdersResponse GetPurchaseOrderByCompanyCode(string companyCode)
        {
            var response = new PurchaseOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetPurchaseOrderByCompanyCode(companyCode);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.PurchaseOrders = Converter.ConvertToPurchaseOrderList(purchaseOrders, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// Gets the Purchase order detail by company code and purchase order number at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="purchaseOrderNumber">purchase order number as string</param>
        /// <returns></returns>
        public PurchaseOrderResponse GetPurchaseOrderByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber)
        {
            var response = new PurchaseOrderResponse();

            var purchaseOrders = _dataLayerContext.GetPurchaseOrderByPurchaseOrderNumber(companyCode, purchaseOrderNumber);
            if (purchaseOrders != null)
            {
                response.PurchaseOrder = Converter.ConvertToPurchaseOrderModel(purchaseOrders, companyCode);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and order type at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="orderType">type of order as string</param>
        /// <returns></returns>
        public PurchaseOrdersResponse GetPurchaseOrderByOrderType(string companyCode, string orderType)
        {
            var response = new PurchaseOrdersResponse();

            var purchaseOrders = _dataLayerContext.GetPurchaseOrderByOrderType(companyCode, orderType);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.PurchaseOrders = Converter.ConvertToPurchaseOrderList(purchaseOrders, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and customer name at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="customerName">name of customer as string</param>
        /// <returns></returns>
        public PurchaseOrderCustomersResponse GetPurchaseOrdersByCustomerName(string companyCode, string customerName)
        {
            var response = new PurchaseOrderCustomersResponse();
            var purchaseOrderCustomers = _dataLayerContext.GetPurchaseOrderByCustomerName(companyCode, customerName);
            if (purchaseOrderCustomers != null && purchaseOrderCustomers.Any())
            {
                response.PurchaseOrderCustomers = Converter.ConvertToPurchaseOrderCustomerModel(purchaseOrderCustomers,
                    companyCode).ToList();
            }
            else
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            return response;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and project number at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="projectNumber">project number as string</param>
        /// <returns></returns>
        public PurchaseOrdersResponse GetPurchaseOrderByProjectNumber(string companyCode, string projectNumber)
        {
            var response = new PurchaseOrdersResponse();

            var purchaseOrders = _dataLayerContext.GetPurchaseOrderByProjectNumber(companyCode, projectNumber);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.PurchaseOrders = Converter.ConvertToPurchaseOrderList(purchaseOrders, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and delivery start and end date range at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="deliveryStartDate">Deliery start date as string</param>
        /// <param name="deliveryEndDate">Delivery end date as string</param>
        /// <returns>return PurchaseOrdersResponse in response</returns>
        public PurchaseOrdersResponse GetPurchaseOrderByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate)
        {
            var response = new PurchaseOrdersResponse();
            var purchaseOrders = _dataLayerContext.GetPurchaseOrderByDeliveryDateRange(companyCode, deliveryStartDate, deliveryEndDate);
            if (purchaseOrders != null && purchaseOrders.Any())
            {
                response.PurchaseOrders = Converter.ConvertToPurchaseOrderList(purchaseOrders, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and order start and end date range at business logic
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="orderStartDate">order start date as string</param>
        /// <param name="orderEndDate">order end date as string</param>
        /// <returns>return PurchaseOrdersResponse in response</returns>
        public PurchaseOrdersResponse GetPurchaseOrderByOrderDateRange(string companyCode, string orderStartDate,
            string orderEndDate)
        {
            var response = new PurchaseOrdersResponse();
            var purchaseorders = _dataLayerContext.GetPurchaseOrderByOrderDateRange(companyCode, orderStartDate,
                orderEndDate);
            if (purchaseorders != null && purchaseorders.Any())
            {
                response.PurchaseOrders = Converter.ConvertToPurchaseOrderList(purchaseorders, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
    }
}