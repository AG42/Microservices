using CreditStatus.Model.Response;

namespace CreditStatus.BusinessLayer.Interfaces
{
    public interface ICreditStatusManager
    {
        CreditsResponse GetCreditStatusByCompanyCode(string companyCode, bool ledgerFlag);
        CreditResponse GetCreditStatusByCustomerCode(string companyCode,string customerCode,bool ledgerflag);
        CreditsResponse GetCreditStatusByCustomerName(string companyCode,string customerName, bool ledgerflag);
    }
}
