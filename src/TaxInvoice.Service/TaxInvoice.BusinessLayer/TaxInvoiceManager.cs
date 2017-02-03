using TaxInvoice.BusinessLayer.Interfaces;
using System.Linq;
using TaxInvoice.Model.Response;
using TaxInvoice.Common;
using TaxInvoice.DataAccessLayer.Interface;
using TaxInvoice.Common.Error;

namespace TaxInvoice.BusinessLayer
{
    public class TaxInvoiceManager : ITaxInvoiceManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public TaxInvoiceManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }

        /// <summary>
        /// This method gets Tax invoice details based on company code in Business layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <returns>Return object of TaxInvoiceResponses</returns>
        public TaxInvoiceResponses GetTaxInvoiceByCompanyCode(string companyCode)
        {
            var response = new TaxInvoiceResponses();
            var taxInvoice = _dataLayerContext.GetTaxInvoiceByCompanyCode(companyCode);
            if (taxInvoice != null && taxInvoice.Any())
            {
                response.TaxInvoices = Converter.ConvertToTaxInvoice(taxInvoice);

            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and Invoice number in business layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="invoiceNo">Invoice Number as string</param>
        /// <returns>Return object of TaxInvoiceResponses</returns>
        public TaxInvoiceResponses GetTaxInvoiceByInvoiceNo(string companyCode, string invoiceNo)
        {
            var response = new TaxInvoiceResponses();

            var taxInvoice = _dataLayerContext.GetTaxInvoiceByInvoiceNo(companyCode, invoiceNo);
            if (taxInvoice != null && taxInvoice.Any())
            {
                response.TaxInvoices = Converter.ConvertToTaxInvoice(taxInvoice);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and Customer code in business layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="customerCode">Cuastomer Code as string</param>
        /// <returns>Return object of TaxInvoiceResponses</returns>
        public TaxInvoiceResponses GetTaxInvoiceByCustomerCode(string companyCode, string customerCode)
        {
            var response = new TaxInvoiceResponses();
            var taxInvoice = _dataLayerContext.GetTaxInvoiceByCustomerCode(companyCode, customerCode);
            if (taxInvoice != null && taxInvoice.Any())
            {
                response.TaxInvoices = Converter.ConvertToTaxInvoice(taxInvoice);

            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and range of Tax amount in business layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="minTaxInvoice">Minimum tax amount as decimal</param>
        /// <param name="maxTaxInvoice">Maximum tax amount as decimal</param>
        /// <returns>Return object of TaxInvoiceResponses</returns>
        public TaxInvoiceResponses GetTaxInvoiceByTaxAmountRange(string companyCode, decimal minTaxAmount, decimal maxTaxAmount)
        {
            var response = new TaxInvoiceResponses();

            var taxInvoices = _dataLayerContext.GetTaxInvoiceByTaxAmountRange(companyCode, minTaxAmount, maxTaxAmount);
            if (taxInvoices != null && taxInvoices.Any())
            {
                response.TaxInvoices = Converter.ConvertToTaxInvoice(taxInvoices);
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
    }
}