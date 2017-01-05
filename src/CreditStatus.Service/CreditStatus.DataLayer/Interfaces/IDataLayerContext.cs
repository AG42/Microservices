using CreditStatus.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace CreditStatus.DataLayer.Interfaces
{
   public interface IDataLayerContext
    {
        IEnumerable<Sl01> GetCreditStatusByCompanyCode(string companyCode);

        Sl01 GetCreditStatusByCustomerCode(string companyCode,string customerCode);

        IEnumerable<Sl01> GetCreditStatusByCustomerName(string companyCode,string customerName);
    }
}
