using System;
using System.Collections.Generic;
using System.Linq;
using PurchaseOrderBySupplier.DataLayer.Entities.Datalake;
using PurchaseOrderBySupplier.Model.Models;

namespace PurchaseOrderBySupplier.BusinessLayer
{
    public class Converter
    {
        /// <summary>
        /// Converting pc12 to Purchase order model
        /// </summary>
        /// <param name="purchasePc12"></param>
        /// <param name="companyCode"></param>
        /// <returns>Returns Purchase Order Model</returns>
        public static Invoice ConvertToPurchaseOrderInvoice(Pc12 purchasePc12, string companyCode)
        {
            decimal outAmountLcu;
            decimal.TryParse(purchasePc12.Pc12010, out outAmountLcu);
            decimal outSalesTaxAmount;
            decimal.TryParse(purchasePc12.Pc12012, out outSalesTaxAmount);
            // Total Amount = AMountLCU + Sales Tax Amount
            decimal total = outAmountLcu + outSalesTaxAmount;
            return new Invoice()
            {
                SupplierInvoiceNumber = purchasePc12.Pc12008.Trim(),
                Total = Math.Round(total,3),
                AmountLCU = Math.Round(outAmountLcu,3),
                SalesTaxAmount = Math.Round(outSalesTaxAmount,3),
                VatCode = purchasePc12.Pc12013
            };
        }
        /// <summary>
        /// Convert Purchase Order Pc12 to Supplier details 
        /// </summary>
        /// <param name="purchasePc12S"></param>
        /// <param name="companyCode"></param>
        /// <returns>Returns list of suppliers</returns>
        public static IEnumerable<Supplier> ConvertToSuppliers(IEnumerable<Pc12> purchasePc12S, string companyCode)
        {
            var suppliersPc12 = purchasePc12S.GroupBy(po => po.Pc12007).OrderBy(p1=>p1.Key);
            foreach (var supplierPc12 in suppliersPc12)
            {
                var supplier = new Supplier
                {
                    PurchaseOrders = new List<PurchaseOrder>(),
                    SupplierCode = supplierPc12.Key.Trim()
                };
                var purchaseOrders = supplierPc12.GroupBy(po => po.Pc12001);
                foreach (var purchaseOr in purchaseOrders)
                {
                    var purchaseOrder = new PurchaseOrder
                    {
                        PurchaseOrderNumber = purchaseOr.Key.Trim(),
                        Invoices = new List<Invoice>()
                    };
                    foreach (var invoice in purchaseOr)
                    {
                        purchaseOrder.Invoices.Add(ConvertToPurchaseOrderInvoice(invoice, companyCode));
                    }
                    supplier.PurchaseOrders.Add(purchaseOrder);
                }
                yield return supplier;
            }
        }
    }
}
