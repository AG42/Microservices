using PurchaseOrderBySupplier.BusinessLayer.Interfaces;
using System.Linq;
using PurchaseOrderBySupplier.Common;
using PurchaseOrderBySupplier.DataLayer.Interfaces;
using PurchaseOrderBySupplier.Model.Response;
using PurchaseOrderBySupplier.Common.Error;

namespace PurchaseOrderBySupplier.BusinessLayer
{
    public class PurchaseOrderBySupplierManager : IPurchaseOrderBySupplierManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public PurchaseOrderBySupplierManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }
        /// <summary>
        /// Get Supplier wise purchase order details for a given company code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>PurchaseOrdersBySupplierResponse</returns>
        public PurchaseOrdersBySupplierResponse GetPurchaseOrdersByCompanyCode(string companyCode)
        {
            var response = new PurchaseOrdersBySupplierResponse();
            var suppilers = _dataLayerContext.GetPurchaseOrdersByCompanyCode(companyCode);
            if (suppilers != null && suppilers.Any())
            {
                response.PurchaseOrdersBySupplier = Converter.ConvertToSuppliers(suppilers, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
        /// <summary>
        /// Get Supplier wise purchase order details for a given supplier code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="supplierCode"></param>
        /// <returns>PurchaseOrdersBySupplierResponse</returns>
        public PurchaseOrdersBySupplierResponse GetPurchaseOrdersBySupplierCode(string companyCode, string supplierCode)
        {
            var response = new PurchaseOrdersBySupplierResponse();
            var suppilers = _dataLayerContext.GetPurchaseOrdersBySupplierCode(companyCode,supplierCode);
            if (suppilers != null && suppilers.Any())
            {
                response.PurchaseOrdersBySupplier = Converter.ConvertToSuppliers(suppilers, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
        /// <summary>
        /// Get Supplier wise purchase order details for a given supplier invoice number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="supplierInvoiceNumber"></param>
        /// <returns>PurchaseOrdersBySupplierResponse</returns>
        public PurchaseOrdersBySupplierResponse GetPurchaseOrdersBySupplierInvoiceNumber(string companyCode, string supplierInvoiceNumber)
        {
            var response = new PurchaseOrdersBySupplierResponse();
            var suppilers = _dataLayerContext.GetPurchaseOrdersBySupplierInvoiceNumber(companyCode, supplierInvoiceNumber);
            if (suppilers != null && suppilers.Any())
            {
                response.PurchaseOrdersBySupplier = Converter.ConvertToSuppliers(suppilers, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
        /// <summary>
        /// Get Supplier wise purchase order details for a given vat code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="vatCode"></param>
        /// <returns>PurchaseOrdersBySupplierResponse</returns>
        public PurchaseOrdersBySupplierResponse GetPurchaseOrdersByVATCode(string companyCode, string vatCode)
        {
            var response = new PurchaseOrdersBySupplierResponse();
            var suppilers = _dataLayerContext.GetPurchaseOrdersByVATCode(companyCode, vatCode);
            if (suppilers != null && suppilers.Any())
            {
                response.PurchaseOrdersBySupplier = Converter.ConvertToSuppliers(suppilers, companyCode).ToList();
            }
            else
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }
            return response;
        }
    }
}
