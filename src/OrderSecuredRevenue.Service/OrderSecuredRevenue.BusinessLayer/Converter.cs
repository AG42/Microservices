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
        public static OrderSecuredRevenueDetails Convert(IEnumerable<OR03> orderRevenues, string companyCode, string orderNo)
        {
            var ordersecuredRevenueModels = new List<OrderSecuredRevenueModel>();
            OrderSecuredRevenueDetails orderDetails = new OrderSecuredRevenueDetails();
            foreach (var orderRevenue in orderRevenues)
                ordersecuredRevenueModels.Add(Convert(orderRevenue, companyCode,orderNo));

            var orderLineRevenue = (from lineitem in ordersecuredRevenueModels
                          group lineitem by lineitem.Order_Number into g
                          select new OrderSecuredRevenueDetails
                          {
                              Order_Number = orderNo,
                              Revenue = g.Sum(_ => _.Revenue)
                          }).FirstOrDefault();


            orderDetails.Order_Number = orderNo;
            orderDetails.Revenue = orderLineRevenue.Revenue;
            orderDetails.OrderSecuredRevenueModelCollection.AddRange(ordersecuredRevenueModels);

            return orderDetails;
        }

        public static OrderSecuredRevenueModel Convert(OR03 or03, string companyCode,string orderNo)
        {
            return new OrderSecuredRevenueModel()
            {
                Company_Code = companyCode,
                Order_Number = or03.or03001,
                Line_Number = or03.or03002,
                Line_Type = or03.or03004== "1"? Constants.NonStockItemLine: Constants.NormalOrderLine,
                //Invoice_Number = or21.or21065,
                Unit_Price = !string.IsNullOrEmpty(or03.or03008) ? or03.or03008 : null,
                Unit_Cost_Price = !string.IsNullOrEmpty(or03.or03009) ? or03.or03009 : null,
                Qty_Ordered = !string.IsNullOrEmpty(or03.or03011) ? or03.or03011 : null,
                Revenue = CalculateRevenue(string.IsNullOrWhiteSpace(or03.or03011) ? 0 : System.Convert.ToDouble(or03.or03011), string.IsNullOrWhiteSpace(or03.or03008) ? 0 : System.Convert.ToDouble(or03.or03008)),
            };
        }

        public static double CalculateRevenue(double quantity, double unitPrice)
        {
            return quantity * unitPrice;
        }

    }
}
