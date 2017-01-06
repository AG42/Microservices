using System.Linq;
using OrderSecuredRevenue.BusinessLayer.Interface;
using OrderSecuredRevenue.DataLayer.Interfaces;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using OrderSecuredRevenue.Model.Responses;
using OrderSecuredRevenue.Common.Error;
using OrderSecuredRevenue.Common;
using OrderSecuredRevenue.Common.Logger;

namespace OrderSecuredRevenue.BusinessLayer
{
    public class OrderSecuredRevenueManager : IOrderSecuredRevenueManager
    {
        private readonly IDatabaseContext _databaseContext;

        public OrderSecuredRevenueManager(IDatabaseContext databaseContext)
        {
            // create ICustomer instance -Data Layer
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Get revenue by invoice number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        //public OrderSecuredRevenueByInvoiceNumberReponse GetOrderSecuredRevenueByInvoiceNumber(string companyCode, string invoiceNumber)
        //{
        //    ApplicationLogger.InfoLogger($"Business Method Name: GetOrderSecuredRevenueByInvoiceNumber :: OrderSecuredRevenue Input: companyCode: [{companyCode}]");
        //    var response = new OrderSecuredRevenueByInvoiceNumberReponse();
        //    var result = _databaseContext.GetOrderSecuredRevenueByInvoiceNumber(companyCode, invoiceNumber);
        //    if (result == null && !result.Any())
        //    {
        //        ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
        //        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
        //        return response;
        //    }

        //    ApplicationLogger.InfoLogger($"Data Object: {result}");
        //    response.OrderSecuredRevenueModels.AddRange(Converter.Convert(result, companyCode, invoiceNumber));
        //    ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

        //    return response;
        //}

        /// <summary>
        /// Get Revenue by Order number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public OrderSecuredRevenueByOrderNoResponse GetOrderSecuredRevenueByOrderNumber(string companyCode, string orderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrderSecuredRevenueByOrderNo :: OrderSecuredRevenue Input: companyCode: [{companyCode}]");
            var response = new OrderSecuredRevenueByOrderNoResponse();
            var result = _databaseContext.GetOrderSecuredRevenueByOrderNumber(companyCode, orderNo);
            if (result == null || !result.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {result}");
            var salesOrderLineDetails = Converter.ConvertLineDetails(result, companyCode, orderNo);
            response.OrderSecuredRevenueModel = Converter.Convert(salesOrderLineDetails, orderNo);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }
                
        public SalesOrderDetailsByOrderNoResponse GetOrderDetailsByOrderNumber(string companyCode, string orderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrderDetailsByOrderNumber :: OrderSecuredRevenue Input: companyCode: [{companyCode}]");
            var response = new SalesOrderDetailsByOrderNoResponse();
            var salesOrderHeadInfo = _databaseContext.GetSalesOrderDetailsByOrderNumber(companyCode, orderNo);
            if (salesOrderHeadInfo == null || !salesOrderHeadInfo.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {salesOrderHeadInfo}");

            response.SalesOrderDetails = new Model.SalesOrderDetailsModel()
            {
                SalesOrderDetails = Converter.Convert(salesOrderHeadInfo.FirstOrDefault(), companyCode, orderNo)
            };
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            var salesOrderLineDetails = _databaseContext.GetSalesOrderLineDetailsByOrderNumber(companyCode, orderNo);
            ApplicationLogger.InfoLogger($"Data Object: {salesOrderLineDetails}");
            if (salesOrderLineDetails == null || !salesOrderLineDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            response.SalesOrderDetails.SalesOrderDetails.SalesOrderLineDetailsList.AddRange(Converter.ConvertLineDetails(salesOrderLineDetails, companyCode, orderNo));

            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }

        public OrderTypeByOrderNoResponse GetOrderTypeByOrderNumber(string companyCode, string orderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrderTypeByOrderNumber :: OrderSecuredRevenue Input: companyCode: [{companyCode}]");
            var response = new OrderTypeByOrderNoResponse();
            var result = _databaseContext.GetSalesOrderDetailsByOrderNumber(companyCode, orderNo);
            if (result == null || !result.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {result}");
            //var salesOrderLineDetails = Converter.ConvertLineDetails(result, companyCode, orderNo);
            response.OrderType = Converter.GetOrderType(System.Convert.ToInt32(result.FirstOrDefault().or01002));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }

        public OrderDeliveryDateByOrderNoResponse GetOrderDeliveryDateByOrderNumber(string companyCode, string orderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrderTypeByOrderNumber :: OrderSecuredRevenue Input: companyCode: [{companyCode}]");
            var response = new OrderDeliveryDateByOrderNoResponse();
            var result = _databaseContext.GetSalesOrderDetailsByOrderNumber(companyCode, orderNo);
            if (result == null || !result.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {result}");
            //var salesOrderLineDetails = Converter.ConvertLineDetails(result, companyCode, orderNo);
            response.DeliveryDate = System.Convert.ToDateTime(result.FirstOrDefault().or01016);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }
    }
}
