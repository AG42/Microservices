using CreditStatus.BusinessLayer.Interfaces;
using CreditStatus.Common;
using CreditStatus.Common.Error;
using CreditStatus.DataLayer.Interfaces;
using CreditStatus.Model.Response;
using System.Linq;

namespace CreditStatus.BusinessLayer
{
   public class CreditStatusManager: ICreditStatusManager
    {
        private readonly IDataLayerContext _dataLayerContext;

        public CreditStatusManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }

        public CreditsResponse GetCreditStatusByCompanyCode(string companyCode, bool ledgerFlag)
        {
            var response = new CreditsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response))
            {
                var credits = _dataLayerContext.GetCreditStatusByCompanyCode(companyCode);
                if (credits != null && credits.Any())
                {
                    response.Credits = Converter.ConvertToCredits(credits, companyCode, ledgerFlag);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        public CreditResponse GetCreditStatusByCustomerCode(string companyCode, string customerCode, bool ledgerFlag)
        {
            var response = new CreditResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && (!InputValidation.ValidateCustomerCode(customerCode,response)))
            {
                var contracts = _dataLayerContext.GetCreditStatusByCustomerCode(companyCode, customerCode);
                if (contracts != null)
                {
                   response.Credit= Converter.ConvertToCredit(contracts,companyCode, ledgerFlag);
                }
                else
                {
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }
            }
            return response;
        }
        public CreditsResponse GetCreditStatusByCustomerName(string companyCode, string customerName, bool ledgerFlag)
        {
            var response = new CreditsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && (!InputValidation.ValidateCustomerName(customerName, response)))
            {
                var creditStatuses= _dataLayerContext.GetCreditStatusByCustomerName(companyCode, customerName);
                if (creditStatuses != null && creditStatuses.Any())
                {
                    response.Credits = Converter.ConvertToCredits(creditStatuses, companyCode, ledgerFlag);
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
