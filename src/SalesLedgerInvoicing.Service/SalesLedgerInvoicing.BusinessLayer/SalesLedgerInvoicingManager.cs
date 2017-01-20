using SalesLedgerInvoicing.Common;
using SalesLedgerInvoicing.BusinessLayer.Interface;
using SalesLedgerInvoicing.DataLayer.Interfaces;
using SalesLedgerInvoicing.Common.Error;
using SalesLedgerInvoicing.Common.Logger;
using SalesLedgerInvoicing.Model.Responses;
using System.Linq;

namespace SalesLedgerInvoicing.BusinessLayer
{
    public class SalesLedgerInvoicingManager : ISalesLedgerInvoicingManager
    {
        private readonly IDatabaseContext _databaseContext;

        public SalesLedgerInvoicingManager(IDatabaseContext databaseContext)
        {
            // create ISalesLedger instance -Data Layer
            _databaseContext = databaseContext;
        }

        public SalesLedgerInvoicingByCustomerCodeResponse GetSalesLedgerInvoicingByCustomerCode(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetSalesLedgerInvoicingByCustomerCode :: SalesLedgerInvoicing Input: companyCode: [{companyCode}]");
            var response = new SalesLedgerInvoicingByCustomerCodeResponse();
            var customerDetails = _databaseContext.GetCustomerDetailsByCustomerCodeAsync(companyCode, customerCode).Result;
            var salesLedgerInvoicingDetails = _databaseContext.GetSalesLedgerInvoicesByCustomerCodeAsync(companyCode, customerCode).Result;
            if (salesLedgerInvoicingDetails == null || !salesLedgerInvoicingDetails.Any() || customerDetails == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found.");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            ApplicationLogger.InfoLogger($"Data Object: {salesLedgerInvoicingDetails}");
            response.SalesLedgerInvoicingModelList.AddRange(Converter.Convert(salesLedgerInvoicingDetails, companyCode, customerDetails.Sl01002));
            //response.SalesLedgerInvoicingModelList = Converter.Convert(salesOrderLineDetails, orderNo);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }

        public SalesLedgerInvoicingByCustomerNameResponse GetSalesLedgerInvoicingByCustomerName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetSalesLedgerInvoicingByCustomerName :: SalesLedgerInvoicing Input: companyCode: [{companyCode}]");
            var response = new SalesLedgerInvoicingByCustomerNameResponse();
            var customerDetails = _databaseContext.GetCustomerDetailsByNameAsync(companyCode, customerName).Result;
            var salesLedgerInvoicingDetails = _databaseContext.GetSalesLedgerInvoicesByCustomerListAsync(companyCode, customerDetails.ToList()).Result;
            if (salesLedgerInvoicingDetails == null || !salesLedgerInvoicingDetails.Any() || !customerDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found.");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            ApplicationLogger.InfoLogger($"Data Object: {salesLedgerInvoicingDetails}");
            response.SalesLedgerInvoicingModelList.AddRange(Converter.Convert(salesLedgerInvoicingDetails, companyCode, customerName));
            //response.SalesLedgerInvoicingModelList = Converter.Convert(salesOrderLineDetails, orderNo);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }

        public SalesLedgerInvoicingByOrderNumberResponse GetSalesLedgerInvoicingByOrderNumber(string companyCode, string orderNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetSalesLedgerInvoicingByOrderNumber :: SalesLedgerInvoicing Input: companyCode: [{companyCode}]");
            var response = new SalesLedgerInvoicingByOrderNumberResponse();
            var salesLedgerInvoicingDetails = _databaseContext.GetSalesLedgerInvoicesByOrderNumberAsync(companyCode, orderNumber).Result;
            if (salesLedgerInvoicingDetails == null || !salesLedgerInvoicingDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found.");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            ApplicationLogger.InfoLogger($"Data Object: {salesLedgerInvoicingDetails}");

            var customerDetails = _databaseContext.GetCustomerDetailsBySalesLedgerCustomerCodeAsync(companyCode, salesLedgerInvoicingDetails.ToList()).Result;
            response.SalesLedgerInvoicingModelList.AddRange(Converter.Convert(salesLedgerInvoicingDetails, companyCode, customerDetails));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");
            
            return response;
        }

        public SalesLedgerInvoicingByInvoiceNumberResponse GetSalesLedgerInvoicingByInvoiceNumber(string companyCode, string invoiceNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetSalesLedgerInvoicingByInvoiceNumber :: SalesLedgerInvoicing Input: companyCode: [{companyCode}]");
            var response = new SalesLedgerInvoicingByInvoiceNumberResponse();

            var salesLedgerInvoicingDetails = _databaseContext.GetSalesLedgerInvoicesByInvoiceNumberAsync(companyCode, invoiceNumber).Result;
            if (salesLedgerInvoicingDetails == null || !salesLedgerInvoicingDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found.");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            ApplicationLogger.InfoLogger($"Data Object: {salesLedgerInvoicingDetails}");

            var customerDetails = _databaseContext.GetCustomerDetailsBySalesLedgerCustomerCodeAsync(companyCode, salesLedgerInvoicingDetails.ToList()).Result;
            response.SalesLedgerInvoicingModelList.AddRange(Converter.Convert(salesLedgerInvoicingDetails, companyCode,customerDetails));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }
        public SalesLedgerInvoicingByInvoiceDateResponse GetSalesLedgerInvoicingByInvoiceDate(string companyCode, string invoiceFromDate, string invoiceToDate)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetSalesLedgerInvoicingByInvoiceDate :: SalesLedgerInvoicing Input: companyCode: [{companyCode}]");
            var response = new SalesLedgerInvoicingByInvoiceDateResponse();

            var salesLedgerInvoicingDetails = _databaseContext.GetSalesLedgerInvoicesByInvoiceDateRangeAsync(companyCode, invoiceFromDate,invoiceToDate).Result;
            if (salesLedgerInvoicingDetails == null || !salesLedgerInvoicingDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found.");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            ApplicationLogger.InfoLogger($"Data Object: {salesLedgerInvoicingDetails}");

            var customerDetails = _databaseContext.GetCustomerDetailsBySalesLedgerCustomerCodeAsync(companyCode, salesLedgerInvoicingDetails.ToList()).Result;
            response.SalesLedgerInvoicingModelList.AddRange(Converter.Convert(salesLedgerInvoicingDetails, companyCode, customerDetails));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }

    }
}
