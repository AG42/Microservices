using System;
using System.Collections.Generic;
using System.Linq;
using PurchaseLedger.DataLayer.Entities.Datalake;
using PurchaseLedger.Model.Models;

namespace PurchaseLedger.BusinessLayer
{
    public class Converter
    {
        /// <summary>
        /// Converting pl03 to Purchase order model
        /// </summary>
        /// <param name="purchasePl03"></param>
        /// <param name="companyCode"></param>
        /// <returns>Returns Purchase Ledger Model</returns>
        public static Model.Models.PurchaseLedgerModel ConvertToPurchaseOrderInvoice(Pl03 purchasePl03, string companyCode)
        {
            decimal outDiscount1;
            decimal.TryParse(purchasePl03.Pl03008, out outDiscount1);
            decimal outDiscount2;
            decimal.TryParse(purchasePl03.Pl03010, out outDiscount2);
            decimal outDiscount3;
            decimal.TryParse(purchasePl03.Pl03012, out outDiscount3);
            // Total Discount = Discount1 + Discount2 + Discount3
            decimal totalDiscount = outDiscount1 + outDiscount2 + outDiscount3;
            decimal outInvoiceAmt;
            decimal.TryParse(purchasePl03.Pl03014, out outInvoiceAmt);
            decimal outSalesTaxAmt;
            decimal.TryParse(purchasePl03.Pl03016, out outSalesTaxAmt);
            decimal outPaidAmt;
            decimal.TryParse(purchasePl03.Pl03027, out outPaidAmt);
            return new Model.Models.PurchaseLedgerModel()
            {
                InvoiceNo = purchasePl03.Pl03002,
                OrderNo = purchasePl03.Pl03033,
                InvoiceDate = purchasePl03.Pl03004,
                DueDate = purchasePl03.Pl03006,
                LatePaymDate = purchasePl03.Pl03025,
                InvoiceAmt = Math.Round(outInvoiceAmt,3),
                SalesTaxAmt = Math.Round(outSalesTaxAmt),
                PaidAmt = Math.Round(outPaidAmt,3),
                InvoicePaidFlag =purchasePl03.Pl03077,
                Discount = Math.Round(totalDiscount,3)
            };
        }

        /// <summary>
        /// Convert Purchase Ledger Pl03 to Supplier details 
        /// </summary>
        /// <param name="purchasePl03S"></param>
        /// <param name="companyCode"></param>
        /// <returns>Returns list of suppliers</returns>
        public static IEnumerable<Supplier> ConvertToSuppliers(IEnumerable<Pl03> purchasePl03S, string companyCode)
        {
            var suppliersPl03 = purchasePl03S.GroupBy(pl => pl.Pl01001).OrderBy(p1 => p1.Key);
            foreach (var supplierPl03 in suppliersPl03)
            {
                var supplier = new Supplier
                {
                    SupplierCode = supplierPl03.Key.Trim(),
                    PurchaseLedgers = new List<Model.Models.PurchaseLedgerModel>()
                };
                foreach (var purchaseLedger in supplierPl03)
                {
                    supplier.SupplierName = purchaseLedger.Pl01002;
                    supplier.PurchaseLedgers.Add(ConvertToPurchaseOrderInvoice(purchaseLedger, companyCode));
                }
                yield return supplier;
            }
        }
    }
}
