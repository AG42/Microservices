using SalesOrder.Common;
using SalesOrder.Common.Enum;
using SalesOrder.DataLayer.Entities.Datalake;
using SalesOrder.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SalesOrder.BusinessLayer
{
    [ExcludeFromCodeCoverage]
    public class Converter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesOrderOr01"></param>
        /// <returns></returns>
        public static IEnumerable<SalesOrderHeadModel> ConvertToSalesOrderList(IEnumerable<Or01> salesOrderOr01)
        {
            var saleOrders = salesOrderOr01.GroupBy(po => po.Or01001);

            foreach (var saleOrder in saleOrders)
            {
                int outSalesOrderOr01002;
                Int32.TryParse(saleOrder?.FirstOrDefault()?.Or01002, out outSalesOrderOr01002);
                int outSalesOrderOr01008;
                Int32.TryParse(saleOrder?.FirstOrDefault()?.Or01008, out outSalesOrderOr01008);

                    yield return new SalesOrderHeadModel()
                    {
                        OrderNumber = saleOrder?.FirstOrDefault()?.Or01001.Trim(),
                        OrderType = ((OrderType)(outSalesOrderOr01002)).ToString().Replace("_", " "),
                        CustomerCodeInvoice = saleOrder?.FirstOrDefault()?.Or01003.Trim(),
                        FlagPickList = ((FlagPickStatus)(outSalesOrderOr01008)).ToString().Replace("_", " ").Trim(),
                        OrderDate = saleOrder?.FirstOrDefault()?.Or01015.Trim(),
                        DelivaryDate = saleOrder?.FirstOrDefault()?.Or01016.Trim(),
                        OrderDeliveryStatus = GetDeliveryStatusFromResult(saleOrder.Sum(x => Convert.ToInt32(x.Or03011)),
                            saleOrder.Sum(x => Convert.ToInt32(x.Or03021))),

                        SalesOrderLines = saleOrder.Select(x => new SalesOrderLineModel
                        {
                            Description = x.Or03006.Trim(),
                            QuantityOrdered = x.Or03011.Trim(),
                            QuantityShipped = x.Or03012.Trim(),
                            QuantityUniOutForDelivary = x.Or03021.Trim(),
                            DeliveryStatus = GetDeliveryStatusFromResult(Convert.ToInt32(x.Or03011), Convert.ToInt32(x.Or03021))
                        }).ToList()
                    };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantityOrdered"></param>
        /// <param name="quantityUniOutForDelivary"></param>
        /// <returns></returns>
        private static string GetDeliveryStatusFromResult(int quantityOrdered, int quantityUniOutForDelivary)
        {
            string deliveryStatus = string.Empty;

            if (quantityOrdered == quantityUniOutForDelivary)
            {
                deliveryStatus = Constants.Delivered;
            }
            else if (quantityOrdered > quantityUniOutForDelivary)
            {
                deliveryStatus = Constants.PartiallyDelivered;
            }
            else if (quantityUniOutForDelivary == 0)
            {
                deliveryStatus = Constants.Undelivered;
            }
            return deliveryStatus;
        }
    }
}