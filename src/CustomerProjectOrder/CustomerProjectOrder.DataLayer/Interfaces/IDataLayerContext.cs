using System.Collections.Generic;
using CustomerProjectOrder.DataLayer.Entities.Datalake;

namespace CustomerProjectOrder.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        Pr01 GetProjectByNumber(string companyCode, string projectNumber);
        IEnumerable<Pr01> GetProjectByCompanyCode(string companyCode);
        IEnumerable<Pr01> GetProjectByName(string companyCode, string projectName);
        IEnumerable<Pr01> GetProjectByDuration(string companyCode, string startDate, string endDate);
        Pr01 GetProjectByCustomerPONo(string companyCode, string customerPONo);
        IEnumerable<Pr01> GetProjectByAccount(string companyCode, string account);
    }
}
