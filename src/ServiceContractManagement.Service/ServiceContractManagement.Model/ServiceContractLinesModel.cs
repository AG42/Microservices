using ServiceContractManagement.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContractManagement.Model
{
    public class ServiceContractLinesModel
    {
        public string ServiceContractNo { get; set; }
        public string LineNo { get; set; }
        public string UnitPriceOCU { get; set; }
        public string Price_UnitCode { get; set; }
        public string DebitCreditValueOCU { get; set; }
        public string InvoiceQunatity { get; set; }
        public string SalesTaxCode { get; set; }
        public string ActualQuantity { get; set; }
    }
}
