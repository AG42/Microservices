using PurchaseLedger.BusinessLayer.Interfaces;
using PurchaseLedger.DataLayer.Interfaces;
using PurchaseLedger.Model.Response;
using PurchaseLedger.Common.Error;
using PurchaseLedger.Common;
using System.Linq;

namespace PurchaseLedger.BusinessLayer
{
    public class PurchaseLedgerManager : IPurchaseLedgerManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public PurchaseLedgerManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }

        public PurchaseLedgersResponse GetPurchaseLedgerByCompanyCode(string companyCode)
        {
            var response = new PurchaseLedgersResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response))
            {
                var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerByCompanyCode(companyCode);
                if (purchaseLedgers != null && purchaseLedgers.Any())
                {
                    response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        public PurchaseLedgersResponse GetPurchaseLedgerBySupplierCode(string companyCode, string supplierCode)
        {
            var response = new PurchaseLedgersResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(supplierCode, Constants.SupplierCodeRequiredMessage, response))
            {
                var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerBySupplierCode(companyCode, supplierCode);
                if (purchaseLedgers != null && purchaseLedgers.Any())
                {
                    response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        
        /// <summary>
        /// Gets the Purchase ledger details by company code and invoice start and end date range
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="invoiceStartDate"></param>
        /// <param name="invoiceEndDate"></param>
        /// <returns>returns PurchaseLedgers Response</returns>
        public PurchaseLedgersResponse GetPurchaseLedgerByInvoiceDateRange(string companyCode, string invoiceStartDate, string invoiceEndDate)
        {
            var response = new PurchaseLedgersResponse();
            var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerByInvoiceDateRange(companyCode,invoiceStartDate,invoiceEndDate);
            if (purchaseLedgers != null && purchaseLedgers.Any())
            {
                response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
        public PurchaseLedgersResponse GetPurchaseLedgerByInvoiceNo(string companyCode, string InvoiceNo)
        {
            var response = new PurchaseLedgersResponse();
            var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerByInvoiceNo(companyCode, InvoiceNo);
            if (purchaseLedgers != null && purchaseLedgers.Any())
            {
                response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }

        /// <summary>
        /// Gets the Purchase ledger details by company code and order no
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderNo"></param>
        /// <returns>returns PurchaseLedgers Response</returns>
        public PurchaseLedgersResponse GetPurchaseLedgerByOrderNo(string companyCode, string orderNo)
        {
            var response = new PurchaseLedgersResponse();
            var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerByOrderNo(companyCode, orderNo);
            if (purchaseLedgers != null && purchaseLedgers.Any())
            {
                response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
        public PurchaseLedgersResponse GetPurchaseLedgerBySupplierName(string companyCode, string supplierName)
        {
            var response = new PurchaseLedgersResponse();
            var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerBySupplierName(companyCode, supplierName);
            if (purchaseLedgers != null && purchaseLedgers.Any())
            {
                response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="dueStartDate"></param>
        /// <param name="dueEndDate"></param>
        /// <returns></returns>
        public PurchaseLedgersResponse GetPurchaseLedgerByDueDateRange(string companyCode, string dueStartDate, string dueEndDate)
        {
            var response = new PurchaseLedgersResponse();
            var purchaseLedgers = _dataLayerContext.GetPurchaseLedgerByDueDateRange(companyCode, dueStartDate, dueEndDate);
            if (purchaseLedgers != null && purchaseLedgers.Any())
            {
                response.Suppliers = Converter.ConvertToSuppliers(purchaseLedgers, companyCode);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
    }
}
