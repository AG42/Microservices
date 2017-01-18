using OrderSecuredCost.BusinessLayer.Interface;
using OrderSecuredCost.Model.Response;
using OrderSecuredCost.DataLayer.Interfaces;
using OrderSecuredCost.Common;
using OrderSecuredCost.Common.Error;
using System.Linq;

namespace OrderSecuredCost.BusinessLayer
{
    public class OrderSecuredCostManager : IOrderSecuredCostManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public OrderSecuredCostManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }


        /// <summary>
        /// This methode will get all order secured costs details based on company code
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <returns>return object of OrderSecuredCostsResponse</returns>
        public OrderSecuredCostsResponse GetOrderSecuredCostByCompanyCode(string companyCode)
        {
            var response = new OrderSecuredCostsResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response))
            {
                var orderSecuredCosts = _dataLayerContext.GetOrderSecuredCostByCompanyCode(companyCode);
                if (orderSecuredCosts != null && orderSecuredCosts.Any())
                {
                    response.OrderSecuredCosts = Converter.ConvertToOrderSecuredCostList(orderSecuredCosts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }


        /// <summary>
        /// This methode will get all order secured costs details based on given delivery date range
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <param name="deliveryStartDate">delivery start date as string</param>
        /// <param name="deliveryEndDate">delivery end date as string</param>
        /// <returns>return object of OrderSecuredCostsResponse</returns>
        public OrderSecuredCostsResponse GetOrderSecuredCostByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate)
        {
            var response = new OrderSecuredCostsResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(deliveryStartDate, Constants.DateRequiredMessage, response))
            {
                var orderSecuredCosts = _dataLayerContext.GetOrderSecuredCostByDeliveryDateRange(companyCode, deliveryStartDate, deliveryEndDate);
                if (orderSecuredCosts != null && orderSecuredCosts.Any())
                {
                    response.OrderSecuredCosts = Converter.ConvertToOrderSecuredCostList(orderSecuredCosts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }


        /// <summary>
        /// This methode will get all order secured cost based on company code and order cost range
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <param name="minOrderCost">minimum cost of order as string</param>
        /// <param name="maxOrderCost">maximum cost of order as string</param>
        /// <returns>return object of OrderSecuredCostsResponse</returns>
        public OrderSecuredCostsResponse GetOrderSecuredCostByOrderCostRange(string companyCode, string minOrderCost, string maxOrderCost)
        {
            var response = new OrderSecuredCostsResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(minOrderCost, Constants.OrderCostRequiredMessage, response))
            {
                var orderSecuredCosts = _dataLayerContext.GetOrderSecuredCostByOrderCostRange(companyCode, minOrderCost, maxOrderCost);
                if (orderSecuredCosts != null && orderSecuredCosts.Any())
                {
                    response.OrderSecuredCosts = Converter.ConvertToOrderSecuredCostList(orderSecuredCosts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }


        /// <summary>
        /// This methode will get all order secured cost based on company code and order date range
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <param name="orderStartDate">order start date as string</param>
        /// <param name="OrderEndDate">order end date as string</param>
        /// <returns>return object of OrderSecuredCostsResponse</returns>
        public OrderSecuredCostsResponse GetOrderSecuredCostByOrderDateRange(string companyCode, string orderStartDate, string OrderEndDate)
        {
            var response = new OrderSecuredCostsResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(orderStartDate, Constants.DateRequiredMessage, response))
            {
                var orderSecuredCosts = _dataLayerContext.GetOrderSecuredCostByOrderDateRange(companyCode, orderStartDate, OrderEndDate);
                if (orderSecuredCosts != null && orderSecuredCosts.Any())
                {
                    response.OrderSecuredCosts = Converter.ConvertToOrderSecuredCostList(orderSecuredCosts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }


        /// <summary>
        /// This methode will get all order secured cost based on company code and order type
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <param name="orderType">type of order</param>
        /// <returns>return object of OrderSecuredCostsResponse</returns>
        public OrderSecuredCostsResponse GetOrderSecuredCostByOrderType(string companyCode, string orderType)
        {
            var response = new OrderSecuredCostsResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(orderType, Constants.OrderTypeRequiredMessage, response))
            {
                var orderSecuredCosts = _dataLayerContext.GetOrderSecuredCostByOrderType(companyCode, orderType);
                if (orderSecuredCosts != null && orderSecuredCosts.Any())
                {
                    response.OrderSecuredCosts = Converter.ConvertToOrderSecuredCostList(orderSecuredCosts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }


        /// <summary>
        /// This methode will get an order secured cost based on company code and purchase order number
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <param name="purchaseOrderNumber">purchase order number as string</param>
        /// <returns>return object of OrderSecuredCostResponse</returns>
        public OrderSecuredCostResponse GetOrderSecuredCostByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber)
        {
            var response = new OrderSecuredCostResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(purchaseOrderNumber, Constants.PurchaseOrderNoRequiredMessage, response))
            {
                var orderSecuredCost = _dataLayerContext.GetOrderSecuredCostByPurchaseOrderNumber(companyCode, purchaseOrderNumber);
                if (orderSecuredCost != null)
                {
                    response.OrderSecuredCost = Converter.ConvertToOrderSecuredCostModel(orderSecuredCost, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }


        /// <summary>
        /// This methode will get all order secured cost based on company code and user id
        /// </summary>
        /// <param name="companyCode">companycode as string</param>
        /// <param name="userId">user id of user as string</param>
        /// <returns>return object of OrderSecuredCostsResponse</returns>
        public OrderSecuredCostsResponse GetOrderSecuredCostByUserID(string companyCode, string userId)
        {
            var response = new OrderSecuredCostsResponse();

            if (!InputValidation.ValidateNullOrEmpty(companyCode, Constants.CompanyCodeRequiredMessage, response)
                && !InputValidation.ValidateNullOrEmpty(userId, Constants.UserIdRequiredMessage, response))
            {
                var orderSecuredCosts = _dataLayerContext.GetOrderSecuredCostByUserID(companyCode, userId);
                if (orderSecuredCosts != null && orderSecuredCosts.Any())
                {
                    response.OrderSecuredCosts = Converter.ConvertToOrderSecuredCostList(orderSecuredCosts, companyCode);
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