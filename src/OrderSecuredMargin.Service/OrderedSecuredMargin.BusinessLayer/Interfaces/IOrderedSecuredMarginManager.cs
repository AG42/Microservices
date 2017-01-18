using OrderedSecuredMargin.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSecuredMargin.BusinessLayer.Interfaces
{
    public interface IOrderedSecuredMarginManager
    {
        OrderedSecuredMarginResponse GetOrderSecuredMarginByCompanyCode(string companyCode);
        OrderSecureMarginResponse GetOrderSecuredMarginByOrderNo(string companyCode, string orderNo);
        OrderedSecuredMarginResponse GetOrderSecuredMarginByCost(string companyCode, decimal minCost, decimal maxCost);

    }
}
