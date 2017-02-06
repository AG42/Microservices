using ServiceContractManagement.Model.Responses;

namespace ServiceContractManagement.BusinessLayer.Interface
{
    public interface IServiceContractManager
    {
        ServiceContractDetailsByContractDateResponse GetServiceContractDetailsByContractDate(string companyCode,string contractStartDate,string contractEndDate);
        ServiceContractDetailsByContractNumberResponse GetServiceContractDetailsByContractNumber(string companyCode, string contractNumber);
        ServiceContractTypeByContractNumberResponse GetServiceContractTypeByContractNumber(string companyCode, string contractNumber);
        ServiceContractUnitPriceByContractNumberResponse GetServiceContractUnitPriceByContractNumber(string companyCode, string contractNumber);
        ServiceContractInvoiceQuantityByContractNumberResponse GetServiceContractInvoiceQuantityByContractNumber(string companyCode, string contractNumber);
        ServiceContractDebitCreditValueByContractNumberResponse GetServiceContractDebitCreditValueByContractNumber(string companyCode, string contractNumber);
        ServiceContractPaymentTermsByContractNumberResponse GetServiceContractPaymentTermsByContractNumber(string companyCode, string contractNumber);
        ServiceContractSalesmanNoByContractNumberResponse GetServiceContractSalesmanNoByContractNumber(string companyCode, string contractNumber);
        ServiceContractValueByContractNumberResponse GetServiceContractValueByContractNumber(string companyCode, string contractNumber);
    }
}
