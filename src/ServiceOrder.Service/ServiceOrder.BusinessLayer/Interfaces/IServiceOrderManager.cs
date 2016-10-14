using ServiceOrder.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrder.BusinessLayer.Interfaces
{
    public interface IServiceOrderManager
    {
        ServiceOrderByCompanyCodeResponse GetServiceOrderByCompanyCode(string companyCode);
        ServiceOrderByServiceOrderNoResponse GetServiceOrderByServiceOrderNo(string companyCode, string serviceOrderNo);
        ServiceOrderStatusByServiceOrderNoResponse GetServiceOrderStatusByServiceOrderNo(string companyCode, string serviceOrderNo);
        ServiceOrderTypeByServiceOrderNoResponse GetServiceOrderTypeByServiceOrderNo(string companyCode, string serviceOrderNo);
        ServiceOrderByInvoiceCustomerCodeResponse GetServiceOrderByInvoiceCustomerCode(string companyCode, string invoiceCustomerCode);
        ServiceOrderByInvoiceNumberResponse GetServiceOrderByInvoiceNumber(string companyCode, string invoiceNumber);
    }
}
