using OrderSecuredCost.Common.Enum;
using OrderSecuredCost.DataLayer.Entities.Datalake;
using OrderSecuredCost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSecuredCost.BusinessLayer
{
    public class Converter
    {
        public static OrderSecuredCostModel ConvertToOrderSecuredCostModel(Pc01 orderSecuredCostPc01, string companyCode)
        {
            return new OrderSecuredCostModel()
            {
                PurchOrderNo = orderSecuredCostPc01.Pc01001,
                OrderType = ((OrderType)(Convert.ToInt16(orderSecuredCostPc01.Pc01002))).ToString().Replace("_", " "),
                OrderDate = orderSecuredCostPc01.Pc01015,
                DeliveryDate = orderSecuredCostPc01.Pc01016,
                Remark = orderSecuredCostPc01.Pc01060,
                UserID = orderSecuredCostPc01.Pc01073,
                OrderSecuredCost = string.Format("{0:0.00}",(Convert.ToDecimal(orderSecuredCostPc01.Pc01020) + Convert.ToDecimal(orderSecuredCostPc01.Pc01019) + Convert.ToDecimal(orderSecuredCostPc01.Pc01091) + Convert.ToDecimal(orderSecuredCostPc01.Pc01090) + Convert.ToDecimal(orderSecuredCostPc01.Pc01089) + Convert.ToDecimal(orderSecuredCostPc01.Pc01088)))
            };
        }

        public static List<OrderSecuredCostModel> ConvertToOrderSecuredCostList(IEnumerable<Pc01> orderSecuredCostPc01, string companyCode)
        {
            var orderSecuredCostList = new List<OrderSecuredCostModel>();
            foreach (var orderSecuredCostModel in orderSecuredCostPc01)
            {
                orderSecuredCostList.Add(ConvertToOrderSecuredCostModel(orderSecuredCostModel, companyCode));
            }
            return orderSecuredCostList;
        }
    }
}
