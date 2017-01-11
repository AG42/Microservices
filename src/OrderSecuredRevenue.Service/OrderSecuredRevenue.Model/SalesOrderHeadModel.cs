using System;
using System.Collections.Generic;

namespace OrderSecuredRevenue.Model
{
    public class SalesOrderModel
    {
        //OR01001 OrderNumber        
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// OR01001.
        /// </value>
        public string OrderNumber { get; set; }
        //OR01002 OrderType        
        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// OR01002 .
        /// </value>
        public string OrderType { get; set; }

        //OR01007 FLAGOrAckn        
        /// <summary>
        /// Gets or sets the flag or ackn.
        /// </summary>
        /// <value>
        /// OR01007.
        /// </value>
        //public string FLAGOrAckn { get; set; }

        //OR01010 FLAGInvoice
        //public string FLAGInvoice { get; set; }
        //OR01012 TermsPayment
        public string TermsPayment { get; set; }
        //OR01013 TermsofDel
        public string TermsofDelivery { get; set; }
        //OR01014 WayofDel
        public string WayofDelivery { get; set; }
        //OR01015 OrderDate
        public string OrderDate { get; set; }
        //OR01016 DelDate
        public string DeliveryDate { get; set; }
        //OR01019 SalesmanNo
        public string SalesmanNo { get; set; }
        //OR01020 OrdDiscount
        public string OrderDiscount { get; set; }
        //OR01021 InvoiceNo
        public string InvoiceNo { get; set; }
        //OR01024 OrderValue
        public string OrderValue { get; set; }
        //OR01028 CurrCode
        public string CurrCode { get; set; }
        //OR01043 HandlingFee
        public string HandlingFee { get; set; }

        //OR01072 CustPONo
        public string CustPONo { get; set; }
        //OR01091 OrderStatus
        public string OrderStatus { get; set; }

        ////OR01096 OwnerCode
        //public string OwnerCode { get; set; }
        //OR01097 DueDate
        //
        public string DueDate { get; set; }

        //OR01099 SuborderNo
        //public string SuborderNo { get; set; }
        ////OR01137 ProjectNumber
        //public string ProjectNumber { get; set; }
        ////OR01138 SiteCode
        //public string SiteCode { get; set; }
        //OR01139 OriginalInvoiceNo
        //public string OriginalInvoiceNo { get; set; }
        //OR01140 OriginalOrderNo
        //public string OriginalOrderNo { get; set; }
        //OR01156 CancellationCode
        //public string CancellationCode { get; set; }

        //OR01168 InvoiceIssuer
        //public string InvoiceIssuer { get; set; }
        //OR01169 InvCategory
        public string InvoiceCategory { get; set; }
        //OR01170 InvType
        public string InvoiceType { get; set; }
        //OR01214 WayOfPayment
        public string WayOfPayment { get; set; }

        //OR01216 PaymentAddressCode
        public string PaymentAddressCode { get; set; }
        //OR01217 AcceptedDeliveryDate
        public string AcceptedDeliveryDate { get; set; }

        //OR01220 SalesReturnOrderNo
        //public string SalesReturnOrderNo { get; set; }
        //OR01221 SalesReturnInvoiceNo
        //public string SalesReturnInvoiceNo { get; set; }
        ////OR01222 EmailAddress
        //public string EmailAddress { get; set; }
        public List<SalesOrderDetailsLineModel> SalesOrderLineDetailsList { get; } = new List<SalesOrderDetailsLineModel>();

    }
}
