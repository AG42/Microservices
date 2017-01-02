using System;
using System.Collections.Generic;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using OrderSecuredRevenue.Model;
using OrderSecuredRevenue.Common;

namespace OrderSecuredRevenue.BusinessLayer
{
    class Converter
    {
        public static List<OrderSecuredRevenueModel> Convert(IEnumerable<OR03> orderRevenues, string companyCode, string orderNo)
        {
            var ordersecuredRevenueModels = new List<OrderSecuredRevenueModel>();
            foreach (var orderRevenue in orderRevenues)
                ordersecuredRevenueModels.Add(Convert(orderRevenue, companyCode,orderNo));

            return ordersecuredRevenueModels;
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
