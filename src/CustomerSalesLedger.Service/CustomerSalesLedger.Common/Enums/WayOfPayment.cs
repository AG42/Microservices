using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CustomerSalesLedger.Common.Enums
{
    public enum WayOfPayment
    {
        [Description("No automatic payments")]
        NoAutomaticPayments = 0,
        [Description("Transfer to payment ledger (standard)")]
        TransfertoPaymentLedger = 1,
        [Description("German collection method 1")]
        GermanCollectionMethod1 = 2,
        [Description("German collection method 2")]
        GermanCollectionMethod2 = 3,
        [Description("PN/PDC domestic payments")]
        PNPDCDomesticPayments = 4,
        [Description("PN/PDC foreign payments")]
        PNPDCForeignPayments = 5
    }
}
