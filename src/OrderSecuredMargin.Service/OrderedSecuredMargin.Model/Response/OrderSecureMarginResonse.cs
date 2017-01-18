using OrderedSecuredMargin.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSecuredMargin.Model.Response
{
    public class OrderSecureMarginResponse : BaseResponse
    {
        public OrderSecureMarginModel orderSecureMargin { get; set; }
    }
}
