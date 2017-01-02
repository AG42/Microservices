using CreditStatus.Model.Models;
using System.Collections.Generic;

namespace CreditStatus.Model.Response
{
    public class CreditsResponse: BaseResponse
    {
        public IEnumerable<CreditStatusModel> Credits { get; set; }
    }
}
