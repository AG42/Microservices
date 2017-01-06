using ContractInformation.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace ContractInformation.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        IEnumerable<Cmh1Na00> GetContractsByCompanyCode(string companyCode);
        IEnumerable<Cmh1Na00> GetContractsByServiceContractNumber(string companyCode, string contractNumber);
        IEnumerable<Cmh1Na00> GetContractsByRequestNumber(string companyCode, string requestNumber);

        IEnumerable<Cmh1Na00> GetContractsByCustomerPONumber(string companyCode, string poNumber);

        IEnumerable<Cmh1Na00> GetContractsByCustomerName(string companyCode, string customerName);

        IEnumerable<Cmh1Na00> GetContractsByCustomerReference(string companyCode, string reference);

        IEnumerable<Cmh1Na00> GetContractsByCustomerSearchKey(string companyCode, string searchKey);

        IEnumerable<Cmh1Na00> GetContractsByCustomerNameandStatus(string companyCode, string name, string status);

        IEnumerable<Cmh1Na00> GetContractsByCustomerReferenceandStatus(string companyCode, string reference, string status);

        IEnumerable<Cmh1Na00> GetContractsByCustomerSearchKeyandStatus(string companyCode, string searchKey, string status);

        IEnumerable<Cmh1Na00> GetContractsByStatus(string companyCode, string status);

        IEnumerable<Cmh1Na00> GetContractsByDateRange(string companyCode, string startDate, string endDate);

    }
}
