using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderedSecuredMargin.DataAccessLayer.Entities.Datalake;

namespace OrderedSecuredMargin.DataAccessLayer.Interface
{
    public interface IDataLayerContext
    {
        IEnumerable<Or03> GetOrderSecuredMarginByCompanyCode(string companyCode);
        IEnumerable<Or03> GetOrderSecuredMarginByOrderNo(string companyCode, string orderNo);
        IEnumerable<Or03> GetOrderSecuredMarginByCost(string companyCode, decimal minCost, decimal maxCost);
    }
}
