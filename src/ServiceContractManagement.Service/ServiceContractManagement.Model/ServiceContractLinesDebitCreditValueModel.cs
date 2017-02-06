using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContractManagement.Model.Responses;

namespace ServiceContractManagement.Model
{
    public class ServiceContractLinesDebitCreditValueModel 
    {
        public string ServiceContractNo { get; set; }
        public string LineNumber { get; set; }
        public string DebitCreditValue { get; set; }
    }
}
