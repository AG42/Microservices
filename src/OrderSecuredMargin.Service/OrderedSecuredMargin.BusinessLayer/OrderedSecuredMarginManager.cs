using OrderedSecuredMargin.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderedSecuredMargin.Model.Response;
using OrderedSecuredMargin.Common;
using OrderedSecuredMargin.DataAccessLayer.Interface;
using OrderedSecuredMargin.Common.Error;

namespace OrderedSecuredMargin.BusinessLayer
{
    public class OrderedSecuredMarginManager : IOrderedSecuredMarginManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public OrderedSecuredMarginManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }

        public OrderedSecuredMarginResponse GetOrderSecuredMarginByCompanyCode(string companyCode)
        {
            var response = new OrderedSecuredMarginResponse();
            if (!InputValidation.ValidateIsNullorWhiteSpace(companyCode, Constants.CompanyCodeRequiredMessage, response))
            {
                var order = _dataLayerContext.GetOrderSecuredMarginByCompanyCode(companyCode);
                if (order != null && order.Any())
                {
                    response.orderedSecuredMargin = Converter.ConvertToOrderSecureMargins(order);

                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }

        public OrderedSecuredMarginResponse GetOrderSecuredMarginByCost(string companyCode, decimal minCost, decimal maxCost)
        {
            var response = new OrderedSecuredMarginResponse();
            if (!InputValidation.ValidateIsNullorWhiteSpace(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && (!InputValidation.ValidateIsNullorWhiteSpace(Convert.ToString(minCost), Constants.MinCostRequiredMessage, response)
                && (!InputValidation.ValidateIsNullorWhiteSpace(Convert.ToString(maxCost), Constants.MaxCostRequiredMessage, response))))
            {
                var order = _dataLayerContext.GetOrderSecuredMarginByCost(companyCode, minCost, maxCost);
                if (order != null && order.Any())
                {
                    response.orderedSecuredMargin = Converter.ConvertToOrderSecureMargins(order, true);

                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }

        public OrderSecureMarginResponse GetOrderSecuredMarginByOrderNo(string companyCode, string orderNo)
        {
            var response = new OrderSecureMarginResponse();
            if (!InputValidation.ValidateIsNullorWhiteSpace(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && (!InputValidation.ValidateIsNullorWhiteSpace(orderNo, Constants.OrderNoRequiredMessage, response)))
            {
                var order = _dataLayerContext.GetOrderSecuredMarginByOrderNo(companyCode, orderNo);
                if (order != null)
                {
                    response.orderSecureMargin = Converter.ConvertToOrderSecureMargin(order);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
    }
}
