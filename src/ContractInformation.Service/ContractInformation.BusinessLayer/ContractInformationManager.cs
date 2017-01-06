using System.Linq;
using ContractInformation.BusinessLayer.Interfaces;
using ContractInformation.Common;
using ContractInformation.Common.Error;
using ContractInformation.DataLayer.Interfaces;
using ContractInformation.Model.Response;

namespace ContractInformation.BusinessLayer
{
    public class ContractInformationManager : IContractInformationManager
    {
        private readonly IDataLayerContext _dataLayerContext;
        public ContractInformationManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }

        /// <summary>
        /// Get Contracts by Company Code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>Contracts response</returns>
        public ContractsResponse GetContractsByCompanyCode(string companyCode)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response))
            {
                var contracts = _dataLayerContext.GetContractsByCompanyCode(companyCode);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Company Code and cusotmer Name
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns>Contracts Response</returns>
        public ContractsResponse GetContractsByCustomerName(string companyCode, string customerName)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateCustomerName(customerName, response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerName(companyCode,customerName);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Company Code and cusotmer PO number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="poNumber"></param>
        /// <returns>Contracts Response</returns>
        public ContractsResponse GetContractsByCustomerPONumber(string companyCode, string poNumber)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidatePONumber(poNumber, response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerPONumber(companyCode, poNumber);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Company Code and request number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="requestNumber"></param>
        /// <returns>Contracts Response</returns>
        public ContractsResponse GetContractsByRequestNumber(string companyCode, string requestNumber)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateRequestNumber(requestNumber, response))
            {
                var contracts = _dataLayerContext.GetContractsByRequestNumber(companyCode, requestNumber);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Company Code and contract number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="contractNumber"></param>
        /// <returns>Contracts response</returns>
        public ContractsResponse GetContractsByServiceContractNumber(string companyCode, string contractNumber)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateServiceContractNumber(contractNumber, response))
            {
                var contracts = _dataLayerContext.GetContractsByServiceContractNumber(companyCode, contractNumber);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contarcts by Customer name and Status
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns>Contracts Resposne</returns>
        public ContractsResponse GetContractsByCustomerNameandStatus(string companyCode, string name, string status)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateCustomerName(name, response) &&
                !InputValidation.ValidateStatus(status,response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerNameandStatus(companyCode, name,status);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get contracts by Customer Reference
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="reference"></param>
        /// <returns>Contracts Response</returns>
        public ContractsResponse GetContractsByCustomerReference(string companyCode, string reference)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateReference(reference, response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerReference(companyCode, reference);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts By reference and Status 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="reference"></param>
        /// <param name="status"></param>
        /// <returns>Contracts Response</returns>
        public ContractsResponse GetContractsByCustomerReferenceandStatus(string companyCode, string reference, string status)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateReference(reference, response)
                && !InputValidation.ValidateStatus(status,response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerReferenceandStatus(companyCode, reference,status);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Search Key
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="searchKey"></param>
        /// <returns>Contracts Resposne</returns>
        public ContractsResponse GetContractsByCustomerSearchKey(string companyCode, string searchKey)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateSearchKey(searchKey, response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerSearchKey(companyCode, searchKey);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Search Key and Status 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="status"></param>
        /// <returns>Contracts Resposne</returns>
        public ContractsResponse GetContractsByCustomerSearchKeyandStatus(string companyCode, string searchKey, string status)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateSearchKey(searchKey, response)
                && !InputValidation.ValidateStatus(status, response))
            {
                var contracts = _dataLayerContext.GetContractsByCustomerSearchKeyandStatus(companyCode, searchKey, status);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get contracts Expiring with a specific date duration
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Contracts resposne</returns>
        public ContractsResponse GetContractsByDateRange(string companyCode, string startDate, string endDate)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateDate(startDate, response)
                && !InputValidation.ValidateDate(endDate, response))
            {
                var contracts = _dataLayerContext.GetContractsByDateRange(companyCode, startDate, endDate);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        /// <summary>
        /// Get Contracts by Status 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="status"></param>
        /// <returns>Contracts Resposne</returns>
        public ContractsResponse GetContractsByStatus(string companyCode, string status)
        {
            var response = new ContractsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateStatus(status, response))
            {
                var contracts = _dataLayerContext.GetContractsByStatus(companyCode, status);
                if (contracts != null && contracts.Any())
                {
                    response.Contracts = Converter.ConvertToContracts(contracts, companyCode);
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
