using System;
using System.Collections.Generic;
using System.Linq;
using CustomerProjectOrder.BusinessLayer.Interface;
using CustomerProjectOrder.Common;
using CustomerProjectOrder.Common.Error;
using CustomerProjectOrder.Common.Logger;
using CustomerProjectOrder.DataLayer.Interfaces;
using CustomerProjectOrder.Model;
using CustomerProjectOrder.Model.Response;

namespace CustomerProjectOrder.BusinessLayer
{
   public class CustomerProjectOrderManager: ICustomerProjectOrderManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public CustomerProjectOrderManager(IDataLayerContext dataLayerContext)
        {
            // create ICustomer instance -Data Layer
            _dataLayerContext = dataLayerContext;
        }
        public CustomerProjectOrderResponse GetProjectByNumber(string companyCode, string projectNumber)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetProjectByNumber :: Custome Input: companyCode: [{companyCode}] And ProductCode: [{projectNumber}]");
            var response = new CustomerProjectOrderResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProjectNumber(projectNumber, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode Status: Success");

                // Get Item from Master
               
                    var customerProjectOrderHeader = _dataLayerContext.GetProjectByNumber(companyCode, projectNumber);
                    if (customerProjectOrderHeader != null)
                    {
                        response.CustomerProjectOrder =
                        Converter.ConvertToCustomerProjectOrderModel(customerProjectOrderHeader, companyCode);
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }

                    return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ProductCode Status: Failed");
            return response;
        }

        public CustomerProjectOrdersResponse GetProjectByName(string companyCode, string projectName)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetProjectByName :: Custome Input: CompanyCode: [{companyCode}] And ProjectName: [{projectName}]");
            var response = new CustomerProjectOrdersResponse();
            response.CustomerProjectOrders = new List<CustomerProjectOrderModel>();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProjectName(projectName, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProjectName Status: Success");

                // Get Item from Master
                    var customerProjectOrderHeader = _dataLayerContext.GetProjectByName(companyCode, projectName);
                    if (customerProjectOrderHeader.Any())
                    {
                        response.CustomerProjectOrders.AddRange(Converter.ConvertToCustomerProjectOrderModels(customerProjectOrderHeader, companyCode));
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProjectName Status: Failed");
            return response;
        }

        public CustomerProjectOrdersResponse GetProjectByDuration(string companyCode, string startDate, string endDate)
        {
            ApplicationLogger.InfoLogger(
                 $"Business Method Name: GetProjectByDuration :: Custome Input: companyCode: [{companyCode}] And StartDate: [{startDate}] And EndDate: [{endDate}]");
            var response = new CustomerProjectOrdersResponse();
            response.CustomerProjectOrders = new List<CustomerProjectOrderModel>();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateDuration(startDate, endDate, response))

            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and StartDate and EndDate Status: Success");

                // Get Item from Master
                    var customerProjectOrderHeader = _dataLayerContext.GetProjectByDuration(companyCode, startDate, endDate);
                    if (customerProjectOrderHeader.Any())
                    {
                        response.CustomerProjectOrders.AddRange(Converter.ConvertToCustomerProjectOrderModels(customerProjectOrderHeader, companyCode));
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ValidateProductCode Status: Failed");
            return response;
        }

        public CustomerProjectOrderResponse GetProjectByCustomerPONo(string companyCode, string customerPONo)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetProjectByCustomerPoNo :: Custome Input: companyCode: [{companyCode}] And CustomerPoNo: [{customerPONo}]");
            var response = new CustomerProjectOrderResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateCustomerPONo(customerPONo, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and CustomerPoNo Status: Success");

                // Get Item from Master
                    var customerProjectOrderHeader = _dataLayerContext.GetProjectByCustomerPONo(companyCode, customerPONo);
                    if (customerProjectOrderHeader != null)
                    {
                        response.CustomerProjectOrder =
                        Converter.ConvertToCustomerProjectOrderModel(customerProjectOrderHeader, companyCode);
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and CustomerPoNo Status: Failed");
            return response;

        }

        public CustomerProjectOrdersResponse GetProjectByAccount(string companyCode, string account)
        {
            ApplicationLogger.InfoLogger(
                 $"Business Method Name: GetProjectByAccount :: Custome Input: companyCode: [{companyCode}] And Account: [{account}]");
            var response = new CustomerProjectOrdersResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateAccount(account, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and Account Status: Success");

                // Get Item from Master
                    var customerProjectOrderHeader = _dataLayerContext.GetProjectByAccount(companyCode, account);
                    if (customerProjectOrderHeader != null)
                    {
                        response.CustomerProjectOrders =
                        Converter.ConvertToCustomerProjectOrderModels(customerProjectOrderHeader, companyCode);
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }

                    return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and Account Status: Failed");
            return response;
        }

        public CustomerProjectOrdersResponse GetProjectByCompanyCode(string companyCode)
        {
            ApplicationLogger.InfoLogger(
                 $"Business Method Name: GetProjectByDuration :: Custome Input: companyCode: [{companyCode}]");
            var response = new CustomerProjectOrdersResponse();
            response.CustomerProjectOrders = new List<CustomerProjectOrderModel>();
            if (!InputValidation.ValidateCompanyCode(companyCode, response))

            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and StartDate and EndDate Status: Success");

                // Get Item from Master
                    var customerProjectOrderHeader = _dataLayerContext.GetProjectByCompanyCode(companyCode);
                    if (customerProjectOrderHeader.Any())
                    {
                        response.CustomerProjectOrders.AddRange(Converter.ConvertToCustomerProjectOrderModels(customerProjectOrderHeader, companyCode));
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ValidateProductCode Status: Failed");
            return response;
        }
    }
}
