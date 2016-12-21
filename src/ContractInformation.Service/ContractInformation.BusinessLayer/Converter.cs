
using System.Collections.Generic;
using ContractInformation.DataLayer.Entities.Datalake;
using ContractInformation.Model.Models;

namespace ContractInformation.BusinessLayer
{
    class Converter
    {
        /// <summary>
        /// Convert the Data lake entities list to Contract Business Entities list
        /// </summary>
        /// <param name="contractsCmh1Na00"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static List<Contract> ConvertToContracts(
            IEnumerable<Cmh1Na00> contractsCmh1Na00, string companyCode)
        {
            var contracts = new List<Contract>();
            foreach (var contract in contractsCmh1Na00)
            {
                contracts.Add(ConvertToContract(contract, companyCode));
            }
            return contracts;
        }
        /// <summary>
        /// Convert the Data lake entity to Contract Business Entity
        /// </summary>
        /// <param name="contractCmh1Na00"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static Contract ConvertToContract(Cmh1Na00 contractCmh1Na00, string companyCode)
        {
            return new Contract()
            {
                ServiceContractNumber = contractCmh1Na00.Cmh1001,
                CustomerCodeInvoice = contractCmh1Na00.Cmh1002,
                Remarks = contractCmh1Na00.Cmh1005,
                StartDate = contractCmh1Na00.Cmh1015,
                EndDate = contractCmh1Na00.Cmh1016,
                Type = contractCmh1Na00.Cmh1034,
                Code = contractCmh1Na00.Cmh1068,
                Status = GetContractStatus(contractCmh1Na00.Cmh1091),
                Terms = contractCmh1Na00.Cmh1096,
                TypeWarranty = contractCmh1Na00.Cmh1123,
                //OurReference = contractCmh1Na00.Cmh1018,

                CustomerInformation = new Customer()
                {
                    Name = contractCmh1Na00.Cmh1093,
                    Email = contractCmh1Na00.Cmh1220,
                    Reference = contractCmh1Na00.Cmh1004,
                    RequestNumber = contractCmh1Na00.Cmh1132,
                    SearchKey = contractCmh1Na00.Cmh1093,
                    PurchaseOrderNumber = contractCmh1Na00.Cmh1040
                }
            };
        }

        private static string GetContractStatus(string contractStatusCode)
        {
            var ContractStatus = string.Empty;
            switch (contractStatusCode)
            {
                case "1":
                    ContractStatus = "Draft";
                    break;
                case "2":
                    ContractStatus = "Waiting Approval";
                    break;
                case "3":
                    ContractStatus = "Active";
                    break;
                default:
                    ContractStatus = "NA";
                    break;
            }
            return ContractStatus;
        }
    }
}
