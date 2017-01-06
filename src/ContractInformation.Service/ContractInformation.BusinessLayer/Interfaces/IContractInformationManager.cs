using ContractInformation.Model.Response;

namespace ContractInformation.BusinessLayer.Interfaces
{
    public interface IContractInformationManager
    {
        ContractsResponse GetContractsByCompanyCode(string companyCode);
        ContractsResponse GetContractsByServiceContractNumber(string companyCode, string contractNumber);
        ContractsResponse GetContractsByRequestNumber(string companyCode, string requestNumber);

        ContractsResponse GetContractsByCustomerPONumber(string companyCode, string poNumber);

        ContractsResponse GetContractsByCustomerName(string companyCode, string customerName);

        ContractsResponse GetContractsByCustomerReference(string companyCode, string reference);

        ContractsResponse GetContractsByCustomerSearchKey(string companyCode, string searchKey);

        ContractsResponse GetContractsByCustomerNameandStatus(string companyCode, string name, string status);

        ContractsResponse GetContractsByCustomerReferenceandStatus(string companyCode, string reference, string status);

        ContractsResponse GetContractsByCustomerSearchKeyandStatus(string companyCode, string searchKey, string status);

        ContractsResponse GetContractsByStatus(string companyCode, string status);

        ContractsResponse GetContractsByDateRange(string companyCode, string startDate, string endDate);
    }
}
