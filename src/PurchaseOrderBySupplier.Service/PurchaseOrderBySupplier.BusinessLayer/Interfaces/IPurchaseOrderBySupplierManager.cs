using PurchaseOrderBySupplier.Model.Response;

namespace PurchaseOrderBySupplier.BusinessLayer.Interfaces
{
    public interface IPurchaseOrderBySupplierManager
    {
        PurchaseOrdersBySupplierResponse GetPurchaseOrdersByCompanyCode(string companyCode);
        PurchaseOrdersBySupplierResponse GetPurchaseOrdersBySupplierInvoiceNumber(string companyCode, string supplierInvoiceNumber);

        PurchaseOrdersBySupplierResponse GetPurchaseOrdersBySupplierCode(string companyCode, string supplierCode);

        PurchaseOrdersBySupplierResponse GetPurchaseOrdersByVATCode(string companyCode, string VATCode);
    }
}
