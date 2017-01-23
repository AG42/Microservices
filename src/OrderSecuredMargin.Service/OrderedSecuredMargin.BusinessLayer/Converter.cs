using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderedSecuredMargin.DataAccessLayer.Entities.Datalake;
using OrderedSecuredMargin.Model.Models;

namespace OrderedSecuredMargin.BusinessLayer
{
    class Converter
    {
        #region Methods

        /// <summary>

        /// </summary>
        /// <param name="orderedOR03"></param>
        /// <param name="companyCode"></param>
        /// <param name="ledgerFlag"></param>
        /// <returns></returns>
        //public static List<OrderSecureMarginModel> ConvertToOrderSecuredMarginList(IEnumerable<Or03> orderSecuredMarginOr03, string companyCode)
        //{
        //    var orderSecuredMargin = new List<OrderSecureMarginModel>();
        //    foreach (var orderSecuredMarginModel in orderSecuredMarginOr03)
        //    {
        //        orderSecuredMargin.Add(ConvertToOrderSecureMargin(orderSecuredMarginModel, companyCode));
        //    }
        //    return orderSecuredMargin;

        //}


        public static OrderSecureMarginModel ConvertToOrderSecureMargin(IEnumerable<Or03> orderSecuredMarginOr03)
        {
            return ConvertToOrderSecureMargins(orderSecuredMarginOr03).FirstOrDefault();
        }

        #endregion

        public static List<OrderSecureMarginModel> ConvertToOrderSecureMargins(IEnumerable<Or03> orderSecuredMarginOr03, bool isCalculated = false)
        {
            List<OrderSecureMarginModel> lstOforderSecureMarginModel = new List<OrderSecureMarginModel>();
            if (isCalculated == true)
            {
                return orderSecuredMarginOr03.Select(t => new OrderSecureMarginModel { OrderNo = t.Or03001, MarginPercentage = Math.Round(Convert.ToDecimal(t.MarginPercentage), 3) }).ToList();
            }


            var groupByOrderNo = orderSecuredMarginOr03.GroupBy(t => t.Or03001);
            foreach (var data in groupByOrderNo)
            {
                OrderSecureMarginModel orderSecureMarginModel = new OrderSecureMarginModel();
                orderSecureMarginModel.OrderNo = data.Key;
                decimal unitPrice = 0;
                decimal costPrice = 0;
                foreach (var mn in orderSecuredMarginOr03.Where(t => t.Or03001 == data.Key))
                {
                    unitPrice += Convert.ToDecimal(mn.Or03008) * Convert.ToDecimal(mn.Or03011);
                    costPrice += Convert.ToDecimal(mn.Or03009) * Convert.ToDecimal(mn.Or03011);
                }
                if (unitPrice == 0)
                {
                    orderSecureMarginModel.MarginPercentage = 0;
                }
                else
                {
                    orderSecureMarginModel.MarginPercentage = Math.Round(((unitPrice - costPrice) / unitPrice) * 100, 3);
                }

                lstOforderSecureMarginModel.Add(orderSecureMarginModel);
            }
            return lstOforderSecureMarginModel;
        }
    }
}
