using ServiceContractManagement.DataLayer.Entities.Datalake;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceContractManagement.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        Task<SM11> GetServiceContractDetailsByContractCodeAsync(string companyCode, string contractCode);
        Task<IEnumerable<SM11>> GetServiceContractDetailsByContractDateRangeAsync(string companyCode, string contactStartDate, string contractEndDate);
        Task<IEnumerable<SM13>> GetServiceContractLineDetailsByContractCodeAsync(string companyCode, string contractCode);
        Task<IEnumerable<SM13>> GetServieContractLineDetailsByContractCodeListAsync(string companyCode, List<SM11> contractList);
    }
}
