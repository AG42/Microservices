using CreditStatus.Model.Models;

namespace CreditStatus.Model.Response
{
   public  class CreditResponse : BaseResponse
    {
        public CreditStatusModel Credit { get; set; }
    }
}
