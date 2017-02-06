using System;
using System.Linq;
using System.Collections.Generic;
using ServiceContractManagement.DataLayer.Entities.Datalake;
using ServiceContractManagement.Model;
using ServiceContractManagement.Common;

namespace ServiceContractManagement.BusinessLayer
{
    class Converter
    {
        public static ServiceContractMasterModel Convert(SM11 sm11, string companyCode)
        {
            if (sm11 == null)
                return null;
            return new ServiceContractMasterModel()
            {
                ServiceContractNo = sm11.SM11001,
                CustomerCode = sm11.SM11003,
                CustomerReference = sm11.SM11004,
                PaymantTerms = sm11.SM11011,
                ContractDate = sm11.SM11012,
                InvoicingInterval = sm11.SM11014,
                ContractCode = sm11.SM11034,
                ContractValue = sm11.SM11022,
                ContractStartDate = sm11.SM11015,
                ContractEndDate = sm11.SM11016,
                SalesmanNo = sm11.SM11017,
                OurReference = sm11.SM11018,
                InvoiceCurrencyCode = sm11.SM11023,
                ContractCurrencyCode = sm11.SM11027,
                ContractType = sm11.SM11068,
                CustomerSearchKey = sm11.SM11093
            };
        }
        public static ServiceContractLinesModel Convert(SM13 sm13, string companyCode, string contractCode)
        {
            return new ServiceContractLinesModel()
            {
                ServiceContractNo = sm13.SM13001,
                LineNo = sm13.SM13002,
                UnitPriceOCU = sm13.SM13008,
                SalesTaxCode = sm13.SM13019,
                Price_UnitCode = sm13.SM13042,
                DebitCreditValueOCU = sm13.SM13027,
                InvoiceQunatity = sm13.SM13050,
                ActualQuantity = sm13.SM13045
            };
        }
        public static List<ServiceContractLinesUnitPriceModel> ConvertUnitPrice(IEnumerable<SM13> serviceContractDetails)
        {
            var serviceContractDetailsLineModels = new List<ServiceContractLinesUnitPriceModel>();
            ServiceContractLinesUnitPriceModel orderDetails = new ServiceContractLinesUnitPriceModel();
            foreach (var serviceContractLine in serviceContractDetails)
                serviceContractDetailsLineModels.Add(new ServiceContractLinesUnitPriceModel()
                {
                    ServiceContractNo = serviceContractLine.SM13001,
                    LineNumber = serviceContractLine.SM13002,
                    UnitPriceOCU = serviceContractLine.SM13008
                });

            return serviceContractDetailsLineModels;
        }
        public static List<ServiceContractLinesInvoiceQtyModel> ConvertInvoiceQty(IEnumerable<SM13> serviceContractDetails)
        {
            var serviceContractDetailsLineModels = new List<ServiceContractLinesInvoiceQtyModel>();
            ServiceContractLinesInvoiceQtyModel orderDetails = new ServiceContractLinesInvoiceQtyModel();
            foreach (var serviceContractLine in serviceContractDetails)
                serviceContractDetailsLineModels.Add(new ServiceContractLinesInvoiceQtyModel()
                {
                    ServiceContractNo = serviceContractLine.SM13001,
                    LineNumber = serviceContractLine.SM13002,
                    InvoiceQuantity = serviceContractLine.SM13050,
                    ActualQuantity = serviceContractLine.SM13045
                });

            return serviceContractDetailsLineModels;
        }
        public static List<ServiceContractLinesDebitCreditValueModel> ConvertDebitCreditValue(IEnumerable<SM13> serviceContractDetails)
        {
            var serviceContractDetailsLineModels = new List<ServiceContractLinesDebitCreditValueModel>();
            ServiceContractLinesDebitCreditValueModel orderDetails = new ServiceContractLinesDebitCreditValueModel();
            foreach (var serviceContractLine in serviceContractDetails)
                serviceContractDetailsLineModels.Add(new ServiceContractLinesDebitCreditValueModel()
                {
                    ServiceContractNo = serviceContractLine.SM13001,
                    LineNumber = serviceContractLine.SM13002,
                    DebitCreditValue = serviceContractLine.SM13027
                });

            return serviceContractDetailsLineModels;
        }
        public static List<ServiceContractLinesModel> ConvertLineDetails(IEnumerable<SM13> serviceContractLines, string companyCode, string contractCode)
        {
            var serviceContractDetailsLineModels = new List<ServiceContractLinesModel>();
            ServiceContractLinesModel orderDetails = new ServiceContractLinesModel();
            foreach (var serviceContractLine in serviceContractLines)
                serviceContractDetailsLineModels.Add(Convert(serviceContractLine, companyCode, contractCode));

            return serviceContractDetailsLineModels;
        }
        public static List<ServiceContractMasterModel> ConvertServiceContracts(IEnumerable<SM11> contractMasterDetails, IEnumerable<SM13> contractLinesDetails, string companyCode)
        {
            List<ServiceContractMasterModel> ServiceContractModelList = new List<ServiceContractMasterModel>();
            ServiceContractMasterModel masterModel = new ServiceContractMasterModel();


            foreach (var contractMaster in contractMasterDetails)
            {
                if (contractMaster != null)
                {
                    masterModel = new ServiceContractMasterModel();
                    masterModel = Convert(contractMaster, companyCode);

                    var contractLine = contractLinesDetails.Where(cust => cust.SM13001 == contractMaster.SM11001).ToList();
                    masterModel.ServiceContractLineDetails.AddRange(ConvertLineDetails(contractLine, companyCode, masterModel.ServiceContractNo));
                    ServiceContractModelList.Add(masterModel);
                }
            }

            return ServiceContractModelList;
        }
    }
}
