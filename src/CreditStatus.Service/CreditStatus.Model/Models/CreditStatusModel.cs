using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditStatus.Model.Models
{
   public  class CreditStatusModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public bool Status { get; set; }
    }
}
