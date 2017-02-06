using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContractManagement.Model.Responses;

namespace ServiceContractManagement.Model
{
    public class ServiceContractLinesUnitPriceModel 
    {
        public string ServiceContractNo { get; set; }
        public string LineNumber { get; set; }
        public string UnitPriceOCU { get; set; }
    }
   
}
