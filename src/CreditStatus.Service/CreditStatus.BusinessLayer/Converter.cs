using CreditStatus.DataLayer.Entities.Datalake;
using CreditStatus.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;


namespace CreditStatus.BusinessLayer
{
    class Converter
    {
        #region Member
        #endregion

        #region Methods

        /// <summary>

        /// </summary>
        /// <param name="creditsSl01"></param>
        /// <param name="companyCode"></param>
        /// <param name="ledgerFlag"></param>
        /// <returns></returns>
        public static List<CreditStatusModel> ConvertToCredits(
            IEnumerable<Sl01> creditsSl01, string companyCode, bool ledgerFlag)
        {
            var credits = new List<CreditStatusModel>();
            foreach (var credit in creditsSl01)
            {
                credits.Add(ConvertToCredit(credit, companyCode, ledgerFlag));
            }
            return credits;
        }

    public static CreditStatusModel ConvertToCredit(Sl01 creditsSl01, string companyCode, bool ledgerFlag)
    {
        return new CreditStatusModel()
        {
            CustomerCode = creditsSl01.Sl01001,
            CustomerName = creditsSl01.Sl01002,
            Status = GetCreditStatus(creditsSl01, ledgerFlag)
        };
    }
    private static bool GetCreditStatus(Sl01 creditSl01, bool ledgerFlag)
    {
        double customerBalance = 0.0;
        double unpaidInvoices = 0.0;
        double orderedNotShipped = 0.0;
        double shippedNotInvoiced = 0.0;
        double.TryParse(creditSl01.Sl01038.Trim(), out unpaidInvoices);
        double.TryParse(creditSl01.Sl01057.Trim(), out orderedNotShipped);
        double.TryParse(creditSl01.Sl01058.Trim(), out shippedNotInvoiced);
            bool creditStatusFlag = false;
        // If Input Flag is True then Customer Balance is calculated as
        //Unpaid Invoices (SL01038) + Ordered Not Shipped (SL01057) + Shipped Not Invoiced (SL01058)
        if (ledgerFlag)
        {
            customerBalance = unpaidInvoices + orderedNotShipped + shippedNotInvoiced;
        }
        else // If Input Flag is Flase then Customer Balance is calculated as
             //Unpaid Invoices (SL01038) + Shipped Not Invoiced (SL01058)
        {
            customerBalance = unpaidInvoices + shippedNotInvoiced;
        }

        //The Customer Balance is compared with the customer's Credit Limit (SL01037) and
        //if it is greater, the credit check fails for this customer.
       
        if (Convert.ToDouble(creditSl01.Sl01037.Trim()) >= customerBalance)
        {
            creditStatusFlag = true;
            // return creditStatusFlag;
        }
        
        return creditStatusFlag;
    }

    #endregion
}
}
