using System.Linq;
using ServiceContractManagement.BusinessLayer.Interface;
using ServiceContractManagement.DataLayer.Interfaces;
using ServiceContractManagement.Model.Responses;
using ServiceContractManagement.Common.Logger;
using ServiceContractManagement.Common.Error;
using ServiceContractManagement.Common;

namespace ServiceContractManagement.BusinessLayer
{
    public class ServiceContractManager: IServiceContractManager
    {
        private readonly IDatabaseContext _databaseContext;
        public ServiceContractManager(IDatabaseContext databaseContext)
        {
            // create IServiceContract instance -Data Layer
            _databaseContext = databaseContext;
        }
        public ServiceContractDetailsByContractNumberResponse GetServiceContractDetailsByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDetailsByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractDetailsByContractNumberResponse();
            var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            //var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractCode(companyCode, contractCode);

            if (serviceContractMasterInfo == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            
            ApplicationLogger.InfoLogger($"Data Object: {serviceContractMasterInfo}");
            response.ServiceContractDetails = Converter.Convert(serviceContractMasterInfo, companyCode);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            var serviceContractLineDetails = _databaseContext.GetServiceContractLineDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            ApplicationLogger.InfoLogger($"Data Object: {serviceContractLineDetails}");
            if (serviceContractLineDetails == null || !serviceContractLineDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            response.ServiceContractDetails.ServiceContractLineDetails.AddRange(Converter.ConvertLineDetails(serviceContractLineDetails, companyCode, contractNumber));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;

        }
        public ServiceContractTypeByContractNumberResponse GetServiceContractTypeByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDetailsByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractTypeByContractNumberResponse();
            var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            if (serviceContractMasterInfo == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {serviceContractMasterInfo}");
            if (!string.IsNullOrEmpty(serviceContractMasterInfo.SM11068)) { response.ContractType = serviceContractMasterInfo.SM11068; }
            

            return response;

        }
        public ServiceContractUnitPriceByContractNumberResponse GetServiceContractUnitPriceByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDetailsByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractUnitPriceByContractNumberResponse();

            var serviceContractLineDetails = _databaseContext.GetServiceContractLineDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            ApplicationLogger.InfoLogger($"Data Object: {serviceContractLineDetails}");
            if (serviceContractLineDetails == null || !serviceContractLineDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            response.ServiceContractLineItemUnitPrice.AddRange(Converter.ConvertUnitPrice(serviceContractLineDetails));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;

        }
        public ServiceContractDetailsByContractDateResponse GetServiceContractDetailsByContractDate(string companyCode, string contractFromDate, string contractToDate)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDetailsByContractDate :: SalesLedgerInvoicing Input: companyCode: [{companyCode}]");
            var response = new ServiceContractDetailsByContractDateResponse();

            var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractDateRangeAsync(companyCode, contractFromDate, contractToDate).Result;
            if (serviceContractMasterInfo == null || !serviceContractMasterInfo.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found.");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            ApplicationLogger.InfoLogger($"Data Object: {serviceContractMasterInfo}");

            var contratLineDetailsDetails = _databaseContext.GetServieContractLineDetailsByContractCodeListAsync(companyCode, serviceContractMasterInfo.ToList()).Result;
            ApplicationLogger.InfoLogger($"Data Object: {contratLineDetailsDetails}");
            if (contratLineDetailsDetails == null || !contratLineDetailsDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            response.ServiceContractDetails.AddRange(Converter.ConvertServiceContracts(serviceContractMasterInfo, contratLineDetailsDetails,companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }
        public ServiceContractInvoiceQuantityByContractNumberResponse GetServiceContractInvoiceQuantityByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractInvoiceQuantityByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractInvoiceQuantityByContractNumberResponse();

            var serviceContractLineDetails = _databaseContext.GetServiceContractLineDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            ApplicationLogger.InfoLogger($"Data Object: {serviceContractLineDetails}");
            if (serviceContractLineDetails == null || !serviceContractLineDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            response.ServiceContractLineItemQuantity.AddRange(Converter.ConvertInvoiceQty(serviceContractLineDetails));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }
        public ServiceContractDebitCreditValueByContractNumberResponse GetServiceContractDebitCreditValueByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDebitCreditValueByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractDebitCreditValueByContractNumberResponse();

            var serviceContractLineDetails = _databaseContext.GetServiceContractLineDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            ApplicationLogger.InfoLogger($"Data Object: {serviceContractLineDetails}");
            if (serviceContractLineDetails == null || !serviceContractLineDetails.Any())
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }
            response.ServiceContractLineItemDebitCreditValue.AddRange(Converter.ConvertDebitCreditValue(serviceContractLineDetails));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

            return response;
        }
        public ServiceContractPaymentTermsByContractNumberResponse GetServiceContractPaymentTermsByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDetailsByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractPaymentTermsByContractNumberResponse();
            var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            if (serviceContractMasterInfo == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {serviceContractMasterInfo}");
            if (!string.IsNullOrEmpty(serviceContractMasterInfo.SM11011)) { response.PaymentTerms = serviceContractMasterInfo.SM11011; }

            return response;
        }
        public ServiceContractSalesmanNoByContractNumberResponse GetServiceContractSalesmanNoByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractDetailsByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractSalesmanNoByContractNumberResponse();
            var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            if (serviceContractMasterInfo == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {serviceContractMasterInfo}");
            if (!string.IsNullOrEmpty(serviceContractMasterInfo.SM11017)) { response.SalesmanNo = serviceContractMasterInfo.SM11017; }

            return response;

        }

        public ServiceContractValueByContractNumberResponse GetServiceContractValueByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceContractValueByContractCode :: ServiceContract Input: companyCode: [{companyCode}]");
            var response = new ServiceContractValueByContractNumberResponse();
            var serviceContractMasterInfo = _databaseContext.GetServiceContractDetailsByContractCodeAsync(companyCode, contractNumber).Result;
            if (serviceContractMasterInfo == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Object: {serviceContractMasterInfo}");
            if (!string.IsNullOrEmpty(serviceContractMasterInfo.SM11022)) { response.ContractValue = serviceContractMasterInfo.SM11022; }

            return response;
        }
    }
}
