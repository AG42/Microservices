using PurchaseOrderBySupplier.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace PurchaseOrderBySupplier.DataLayer.Interfaces
{
  public  interface IDataLayerContext
    {
        IEnumerable<Pc12> GetPurchaseOrdersByCompanyCode(string companyCode);
        IEnumerable<Pc12> GetPurchaseOrdersBySupplierInvoiceNumber(string companyCode,string supplierInvoiceNumber);

        IEnumerable<Pc12> GetPurchaseOrdersBySupplierCode(string companyCode,string supplierCode);

        IEnumerable<Pc12> GetPurchaseOrdersByVATCode(string companyCode,string vatCode);
    }
}
