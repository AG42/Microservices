using CreditStatus.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditStatus.Model.Response
{
   public  class CreditResponse : BaseResponse
    {
        public CreditStatusModel Credit { get; set; }
    }
}
