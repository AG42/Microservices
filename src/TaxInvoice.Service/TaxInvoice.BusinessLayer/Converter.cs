using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TaxInvoice.DataAccessLayer.Entities.Datalake;
using TaxInvoice.Model.Models;

namespace TaxInvoice.BusinessLayer
{
    [ExcludeFromCodeCoverage]
    class Converter
    {
        public static IEnumerable<TaxInvoiceModel> ConvertToTaxInvoice(IEnumerable<SL17> taxInvoiceSL17)
        {
            var taxInovices = taxInvoiceSL17.Where(t => string.IsNullOrEmpty(t.SL17003) ? true : (t.SL17003?.ToLower() == "x" || t.SL17003?.ToLower() == "z")).GroupBy(ti => new { ti.SL17001 });


            foreach (var taxinvoice in taxInovices)
            {
                yield return new TaxInvoiceModel()
                {
                    InvoiceNo = taxinvoice.FirstOrDefault().SL17001,
                    CustomerCode = taxinvoice.FirstOrDefault().SL17002,
                    TaxRateCode = taxinvoice.FirstOrDefault().SL17004,
                    TotalBaseAmount = taxinvoice.Sum(x => Convert.ToDecimal(x.SL17007)),
                    TotalTaxAmount = taxinvoice.Sum(x => Convert.ToDecimal(x.SL17008)),
                    TaxType = taxinvoice.FirstOrDefault().SL17020,
                    TotalSTBase = taxinvoice.Sum(x => Convert.ToDecimal(x.SL17038)),
                    VATType = taxinvoice.FirstOrDefault().SL17050,
                    TotalSale = taxinvoice.Sum(x => Convert.ToDecimal(x.SL17007)) + taxinvoice.Sum(x => Convert.ToDecimal(x.SL17008)) - taxinvoice.Sum(x => Convert.ToDecimal(x.SL17038))
                };
            }
        }
    }
}