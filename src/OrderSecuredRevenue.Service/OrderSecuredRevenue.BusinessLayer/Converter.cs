using System;
using System.Linq;
using System.Collections.Generic;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using OrderSecuredRevenue.Model;
using OrderSecuredRevenue.Common;

namespace OrderSecuredRevenue.BusinessLayer
{
    class Converter
    {
        private const string DEFAULT_DATE = "1900-01-01 00:00:00.0";
        //public static OrderSecuredRevenueDetails ConvertRevenueDetails(IEnumerable<OR03> orderRevenues, string companyCode, string orderNo)
        //{
        //    var orderRevenueDetails = new OrderSecuredRevenueDetails();
        //    orderRevenueDetails.OrderSecuredRevenueModelCollection.AddRange(Convert(orderRevenues, companyCode, orderNo));
        //    orderRevenueDetails.Order_Number = orderNo;

        //    var listModels = Convert(orderRevenues, companyCode, orderNo);
        //    var result1 = from line in listModels
        //                  group line by line.Revenue into g
        //                  select new OrderSecuredRevenueDetails
        //                  {
        //                      Order_Number = orderNo,
        //                      Revenue = g.Sum(_ => _.Revenue)
        //                  };

        //    //orderRevenueDetails.Revenue = 
        //    return orderRevenueDetails;
        //}

        // public static List<OrderSecuredRevenueModel> Convert(IEnumerable<OR03> orderRevenues, string companyCode, string orderNo)
        public static List<SalesOrderDetailsLineModel> ConvertLineDetails(IEnumerable<OR03> orderRevenues, string companyCode, string orderNo)
        {
            var salesOrderDetailsLineModels = new List<SalesOrderDetailsLineModel>();
            OrderSecuredRevenueModel orderDetails = new OrderSecuredRevenueModel();
            foreach (var orderRevenue in orderRevenues)
                salesOrderDetailsLineModels.Add(Convert(orderRevenue, companyCode, orderNo));

            return salesOrderDetailsLineModels;
        }

        public static SalesOrderModel Convert(OR01 _or01, string companyCode, string orderNo)
        {
            return new SalesOrderModel() {
                OrderNumber = _or01.or01001,
                OrderType = _or01.or01002,
                TermsPayment = _or01.or01012,
                TermsofDelivery = _or01.or01013,
                WayofDelivery = _or01.or01014,
                OrderDate = _or01.or01015,
                DeliveryDate = _or01.or01016,
                SalesmanNo = _or01.or01019,
                OrderDiscount = _or01.or01020,
                InvoiceNo = _or01.or01021,
                OrderValue = _or01.or01024,
                CurrCode = _or01.or01028,
                HandlingFee = _or01.or01043,
                CustPONo = _or01.or01072,
                OrderStatus = _or01.or01091,
                //SuborderNo = _or01.OR01099,
                //OriginalInvoiceNo = _or01.OR01139,
                //OriginalOrderNo = _or01.OR01140,
                InvoiceIssuer = _or01.or01169,
                InvoiceType = _or01.or01170,
                WayOfPayment = _or01.or01214,
                PaymentAddressCode = _or01.or01216,
                AcceptedDeliveryDate = _or01.or01217
            };

        }

        public static SalesOrderDetailsLineModel Convert(OR03 or03, string companyCode, string orderNo)
        {
            return new SalesOrderDetailsLineModel()
            {
                Company_Code = companyCode,
                Order_Number = or03.or03001,
                Line_Number = or03.or03002,
                Line_Type = or03.or03004 == "1" ? Constants.NonStockItemLine : Constants.NormalOrderLine,
                //Invoice_Number = or21.or21065,
                Unit_Price = !string.IsNullOrEmpty(or03.or03008) ? or03.or03008 : null,
                Unit_Cost_Price = !string.IsNullOrEmpty(or03.or03009) ? or03.or03009 : null,
                Qty_Ordered = !string.IsNullOrEmpty(or03.or03011) ? or03.or03011 : null,
                Revenue = CalculateRevenue(string.IsNullOrWhiteSpace(or03.or03011) ? 0 : System.Convert.ToDouble(or03.or03011), string.IsNullOrWhiteSpace(or03.or03008) ? 0 : System.Convert.ToDouble(or03.or03008)),
            };
        }

        public static OrderSecuredRevenueModel Convert(List<SalesOrderDetailsLineModel> salesOrderDetails, string orderNo)
        {
            var orderLineRevenue = (from lineitem in salesOrderDetails
                                    group lineitem by lineitem.Order_Number into orders
                                    select new OrderSecuredRevenueModel
                                    {
                                        OrderNumber = orderNo,
                                        Revenue = orders.Sum(revenue => revenue.Revenue)
                                    }).FirstOrDefault();

            return new OrderSecuredRevenueModel()
            {
                OrderNumber = orderNo,
                Revenue = orderLineRevenue.Revenue
            };
        }

        public static double CalculateRevenue(double quantity, double unitPrice)
        {
            return quantity * unitPrice;
        }

        public static string GetOrderType(int orderType)
        {
            switch (orderType)
            {
                case 0:
                    return Constants.Quotation;
                case 1:
                    return Constants.NormalOrder;
                case 2:
                    return Constants.InvoiceOrder;
                case 3:
                    return Constants.DirectOrder;
                case 4:
                    return Constants.BackOrder;
                case 5:
                    return Constants.RepeatOrder;
                case 6:
                    return Constants.WorkOrder;
                case 7:
                    return Constants.DirectCreditOrder;
                case 8:
                    return Constants.CreditOrder;
                case 9:
                    return Constants.ReinvoiceOrder;
                default:
                    return string.Empty;
            }

            return "";
        }

        public static DateTimeOffset? GetDeliveryDate(string deliveryDate)
        {
            DateTime defaultDate = System.Convert.ToDateTime(DEFAULT_DATE);
            if (System.Convert.ToDateTime(deliveryDate).Date == defaultDate.Date || string.IsNullOrWhiteSpace(deliveryDate))
                return null;
            return DateTimeOffset.Parse(deliveryDate).UtcDateTime;
        }


    }
}
